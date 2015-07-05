using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Detox.Classes;

namespace DetoxConfig
{
    public partial class frmMain : Form
    {
        static string configFilePath = "detox.config.json";

        public frmMain()
        {
            InitializeComponent();
            Configurations.Instance.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFilePath));
            var config = Configurations.Instance.Current;
            chkCustomBackground.Checked = config.CustomObjects.UseCustomBackgrounds;
            chkCustomFonts.Checked = config.CustomObjects.UseCustomFonts;
            chkCustomIcons.Checked = config.CustomObjects.UseCustomHpMpIcons;
            chkInitSteam.Checked = config.Steam.InitializeSteam;
            chkSkipSplash.Checked = config.Graphics.SkipSplash;
            cmbSkin.Items.Clear();
            cmbSkin.Items.AddRange(GetAvailableSkins().ToArray());
            if(cmbSkin.Items.Contains(config.Graphics.Skin))
            {
                cmbSkin.SelectedIndex = cmbSkin.Items.IndexOf(config.Graphics.Skin);
            }
            chkAutoloadPlugins.Items.Clear();
            var plugins = GetAvailablePlugins();
            chkAutoloadPlugins.Items.AddRange(plugins.ToArray());
            foreach (var p in config.Plugins.AutoLoadPlugins)
            {
                if (plugins.Contains(p))
                {
                    chkAutoloadPlugins.SetItemChecked(plugins.IndexOf(p), true);
                }
            }
            var res = GetAvailableResolutions();
            cmbResolution.Items.Clear();
            cmbResolution.Items.AddRange(res.ToArray());
            var configRes = string.Format("{0}x{1}", config.Graphics.StartupWindowWidth, config.Graphics.StartupWindowHeight);
            if (res.Contains(configRes))
            {
                cmbResolution.SelectedIndex = res.IndexOf(configRes);
            }

        }

        private List<string> GetAvailableSkins()
        {
            var skinDirectory = "DetoxContent\\Skins\\";
            var skins = new List<string>();
            foreach (var file in Directory.GetFiles(skinDirectory))
            {
                if (file.EndsWith(".skin"))
                {
                    skins.Add(Path.GetFileNameWithoutExtension(file));
                }
            }
            return skins;
        }

        private List<string> GetAvailablePlugins()
        {
            var pluginDirectory = "DetoxLibs\\";
            var plugins = new List<string>();
            foreach (var file in Directory.GetFiles(pluginDirectory))
            {
                if (file.EndsWith(".dll"))
                {
                    var result = DetoxAPI.DetoxPluginManager.PluginExists(Path.GetFileNameWithoutExtension(file));
                    if (result == DetoxAPI.DetoxPluginErrorReason.ErrorTypes.Success)
                        plugins.Add(Path.GetFileNameWithoutExtension(file));
                }
            }
            return plugins;
        }

        private List<string> GetAvailableResolutions()
        {
            var resolutions = new List<string>();
            foreach (var dm in Microsoft.Xna.Framework.Graphics.GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                string resStr = string.Format("{0}x{1}", dm.Width, dm.Height);
                if (!resolutions.Contains(resStr))
                    resolutions.Add(resStr);
            }
            return resolutions;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveExit_Click(object sender, EventArgs e)
        {
            SaveConfig();
            this.Close();
        }

        private void SaveConfig()
        {
            var config = Configurations.Instance.Current;
            config.CustomObjects.UseCustomBackgrounds = chkCustomBackground.Checked;
            config.CustomObjects.UseCustomFonts = chkCustomFonts.Checked;
            config.CustomObjects.UseCustomHpMpIcons = chkCustomIcons.Checked;
            config.Graphics.Skin = (string)cmbSkin.SelectedItem;
            config.Graphics.SkipSplash = chkSkipSplash.Checked;
            string res = (string)cmbResolution.SelectedItem;
            config.Graphics.StartupWindowHeight = Int32.Parse(res.Substring(res.IndexOf('x') + 1));
            config.Graphics.StartupWindowWidth = Int32.Parse(res.Substring(0, res.IndexOf('x')));
            config.Plugins.AutoLoadPlugins = chkAutoloadPlugins.CheckedItems.Cast<string>().ToList();
            config.Steam.InitializeSteam = chkInitSteam.Checked;
            Configurations.Instance.SaveConfig(configFilePath);
        }

        private void btnSaveRun_Click(object sender, EventArgs e)
        {
            SaveConfig();
            Program.StartDetox();
            this.Close();
        }
    }
}
