﻿// -- Detox License ------------------------------------------------------
//
//    This file is part of Detox.
//
//    Detox is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    Detox is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with Detox.  If not, see <http://www.gnu.org/licenses/>.
// -----------------------------------------------------------------------

namespace Detox
{
    using Classes;
    using DetoxAPI;
    using Microsoft.Win32;
    using Mono.Cecil;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Diagnostics;

    internal class Program
    {
        //Set to true by the configuration window to indicate that Terraria should be launched
        public static bool RunDetox = false;

        /// <summary>
        /// Default Constructor
        /// </summary>
        static Program()
        {
            // Attach helper events..
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// Main application entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // Store the base path to Detox..
            Detox.DetoxBasePath = AppDomain.CurrentDomain.BaseDirectory;

            bool runArg = args.Contains("-run");

            if (!runArg)
            {
                Logging.Instance.Log("[Detox:Config] Showing configuration window");

                Configurations.Instance.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "detox.config.json"));
                if (!Configurations.Instance.Current.SkipConfig)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new frmConfig());

                    if (!RunDetox)
                    {
                        Logging.Instance.Log("[Detox:Config] Detox exited after configuration at: " + DateTime.Now);
                        return;
                    }
                }

                Process.Start(Assembly.GetExecutingAssembly().Location, "-run");
                return;
            }

            Logging.Instance.Log("[Detox] Detox started at: " + DateTime.Now);

            try
            {
                // See if we have Terraria in the same folder..
                string terrariaPath;
                if (File.Exists("Terraria.exe"))
                {
                    // Load Terraria..
                    terrariaPath = AppDomain.CurrentDomain.BaseDirectory;
                }
                else if (File.Exists("Terraria\\Terraria.exe"))
                {
                    // Load Terraria..
                    terrariaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Terraria");
                }
                else
                {
                    // Attempt to obtain Terraria's path from the registry..
                    terrariaPath = Program.GetValue<string>("HKEY_LOCAL_MACHINE\\SOFTWARE\\Re-Logic\\Terraria", "Install_Path");
                }
                Detox.TerrariaBasePath = terrariaPath;
                Detox.TerrariaAsm = AssemblyDefinition.ReadAssembly(Path.Combine(Detox.TerrariaBasePath, "Terraria.exe"));
            }
            catch
            {
                MessageBox.Show("There was an error locating Terraria.exe", "Detox Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Logging.Instance.Log("[Detox] Detox located Terraria at: " + Detox.TerrariaBasePath);

            // Set the working folder to Terraria's path..
            Directory.SetCurrentDirectory(Detox.TerrariaBasePath);

            // Load the configuration file..
            Logging.Instance.Log("[Detox] Loading configuration file..");
            Configurations.Instance.LoadConfig(AppDomain.CurrentDomain.BaseDirectory + "\\detox.config.json");
            Logging.Instance.Log("[Detox] Configuration file loaded!");

            // Prepare and apply hooks..
            Logging.Instance.Log("[Detox] Registering internal hooks..");
            Events.Initialize();
            Events.eventExceptionHandler = EventExceptionHandler;
            Events.XnaEvents.PreInitialize.Register(null, Hooks.OnXnaPreInitialize, 0);
            Events.XnaEvents.PostInitialize.Register(null, Hooks.OnXnaPostInitialize, 0);
            Events.XnaEvents.PostLoadContent.Register(null, Hooks.OnXnaPostLoadContent, 0);
            Events.XnaEvents.PreUpdate.Register(null, Hooks.OnXnaPreUpdate, 0);
            Events.XnaEvents.PostUpdate.Register(null, Hooks.OnXnaPostUpdate, 0);
            Events.XnaEvents.PreDraw.Register(null, Hooks.OnXnaPreDraw, 0);
            Events.XnaEvents.PostDraw.Register(null, Hooks.OnXnaPostDraw, 0);

            // Apply internal detours..
            Logging.Instance.Log("[Detox] Applying internal detours / patches..");
            (from t in Assembly.GetExecutingAssembly().GetTypes()
             from m in t.GetMethods()
             from a in m.GetCustomAttributes(typeof(InjectionAttribute), false)
             select m).ToList().ForEach(m => m.Invoke(null, new object[] { Detox.TerrariaAsm }));

            // Load auto-start plugins..
            foreach (var s in Configurations.Instance.Current.Plugins.AutoLoadPlugins)
            {
                try
                {
                    DetoxPluginManager.LoadPlugin(s);
                    Logging.Instance.Log("[Detox] Loaded auto-load plugin: " + s);
                }
                catch
                {
                    Logging.Instance.Log("[Detox] Failed to load auto-load plugin: " + s);
                }
            }

            // Invoke Detox Initialize event..
            Events.DetoxEvents.InvokeDetoxInitialize();

            try
            {
                // Initialize Terraria..
                using (var mStream = new MemoryStream())
                {
                    // Write the new file to memory and load..
                    Detox.TerrariaAsm.Write(mStream);
                    Detox.Terraria = Assembly.Load(mStream.GetBuffer());

#if DEBUG
                    // Save a copy for debugging..
                    File.WriteAllBytes("detox.debug.exe", mStream.GetBuffer());
#endif

                    // Attempt to locate the constructor..
                    var mainType = Detox.Terraria.GetType("Terraria.Main");
                    var ctorInfo = mainType.GetConstructor(new Type[] { });
                    if (ctorInfo == null)
                        throw new InvalidOperationException();

                    // Attempt to invoke the constructor..
                    var ctor = ctorInfo.Invoke(null);

                    var socialModeEnum = Detox.Terraria.GetType("Terraria.Social.SocialMode");
                    FieldInfo socialModeInfo;
                    if (Configurations.Instance.Current.Steam.InitializeSteam)
                    {
                        Logging.Instance.Log("[Detox] Initializing Terraria SocialAPI in Steam mode...");
                        Environment.SetEnvironmentVariable("SteamAppId", "105600");
                        Environment.SetEnvironmentVariable("SteamGameId", "105600");
                        socialModeInfo = socialModeEnum.GetField("Steam");
                    }
                    else
                    {
                        Logging.Instance.Log("[Detox] Initializing Terraria SocialAPI...");
                        socialModeInfo = socialModeEnum.GetField("None");
                    }

                    var socialMode = socialModeInfo.GetValue(null);
                    Detox.Terraria.GetType("Terraria.Social.SocialAPI").GetMethod("Initialize").Invoke(null, new object[]
                        {
                            socialMode
                        });

                    Logging.Instance.Log("[Detox] Terraria SocialAPI initialized.");

                    var run = mainType.GetMethod("Run", Detox.BindFlags);
                    if (run == null)
                        throw new InvalidOperationException();

                    // Run Terraria..
                    run.Invoke(ctor, null);
                }
            }
            catch (Exception e)
            {
                Logging.Instance.Log("[Detox] Encountered an error while starting / running Terraria:");
                Logging.Instance.Log(e.ToString());
            }

            // Save the configuration file..
            Logging.Instance.Log("[Detox] Saving configuration file..");
            Configurations.Instance.SaveConfig(AppDomain.CurrentDomain.BaseDirectory + "\\detox.config.json");
            Logging.Instance.Log("[Detox] Configuration file saved!");

            Logging.Instance.Log("[Detox] Detox exited at: " + DateTime.Now);
        }

        private static void EventExceptionHandler(string eventName, Exception e)
        {
            Logging.Instance.Log("[Detox:Events] Encountered an error in Terraria event \"" + eventName + "\":");
            Logging.Instance.Log(e.ToString());
        }

        /// <summary>
        /// Reads a registry value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKeyName"></param>
        /// <param name="strValueName"></param>
        /// <returns></returns>
        public static T GetValue<T>(String strKeyName, String strValueName)
        {
            try
            {
                return (T)Registry.GetValue(strKeyName, strValueName, default(T));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Resolves plugins and other needed assemblies from the DetoxLibs folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private static Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs e)
        {
            try
            {
                var asmPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DetoxLibs", e.Name.Split(',')[0] + ".dll");
                var asm = File.Exists(asmPath) ? Assembly.Load(File.ReadAllBytes(asmPath)) : null;
                if (asm != null)
                    return asm;

                string resourceName = new AssemblyName(e.Name).Name + ".dll";
                string name = Array.Find(Detox.Terraria.GetManifestResourceNames(), element => element.EndsWith(resourceName));
                using (var resourceStream = Detox.Terraria.GetManifestResourceStream(name))
                {
                    byte[] buf = new byte[resourceStream.Length];
                    resourceStream.Read(buf, 0, buf.Length);
                    asm = Assembly.Load(buf);
                }

                return asm;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Catches and logs unhandled exceptions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var crashLog = AppDomain.CurrentDomain.BaseDirectory + "\\detox.crashlog.txt";

            try
            {
                // Write the exception to the log file..
                using (var sWriter = new StreamWriter(crashLog, true))
                {
                    sWriter.WriteLine("!! CRASH !! Date: {0}", DateTime.Now);
                    sWriter.WriteLine("=================================================================================");
                    sWriter.WriteLine(e.ExceptionObject.ToString());
                    sWriter.WriteLine();
                    sWriter.Flush();
                }
            }
            catch
            {
            }

            // Announce the exception and exit..
            var error = string.Format("Detox has encountered a critical error and must exit:\r\n\r\n{0}", e.ExceptionObject);
            MessageBox.Show(error, "Detox :: Critical Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
