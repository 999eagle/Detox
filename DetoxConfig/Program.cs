using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetoxConfig
{
    static class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
        }

        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs e)
        {
            try
            {
                var asmPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DetoxLibs", e.Name.Split(',')[0] + ".dll");
                return File.Exists(asmPath) ? Assembly.Load(File.ReadAllBytes(asmPath)) : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Arguments.Init(args);
            if (Arguments.HasArgument("noconfig"))
            {
                StartDetox();
                return;
            }
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public static void StartDetox()
        {
            Process.Start("Detox.exe", "-noconfig");
        }
    }
}
