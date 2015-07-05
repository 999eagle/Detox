namespace DetoxConfig
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.chkInitSteam = new System.Windows.Forms.CheckBox();
            this.grpGraphics = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSkin = new System.Windows.Forms.ComboBox();
            this.chkSkipSplash = new System.Windows.Forms.CheckBox();
            this.grpObjects = new System.Windows.Forms.GroupBox();
            this.chkCustomBackground = new System.Windows.Forms.CheckBox();
            this.chkCustomFonts = new System.Windows.Forms.CheckBox();
            this.chkCustomIcons = new System.Windows.Forms.CheckBox();
            this.grpPlugins = new System.Windows.Forms.GroupBox();
            this.chkAutoloadPlugins = new System.Windows.Forms.CheckedListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveExit = new System.Windows.Forms.Button();
            this.btnSaveRun = new System.Windows.Forms.Button();
            this.grpGraphics.SuspendLayout();
            this.grpObjects.SuspendLayout();
            this.grpPlugins.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkInitSteam
            // 
            this.chkInitSteam.AutoSize = true;
            this.chkInitSteam.Location = new System.Drawing.Point(12, 12);
            this.chkInitSteam.Name = "chkInitSteam";
            this.chkInitSteam.Size = new System.Drawing.Size(96, 17);
            this.chkInitSteam.TabIndex = 0;
            this.chkInitSteam.Text = "Initialize Steam";
            this.chkInitSteam.UseVisualStyleBackColor = true;
            // 
            // grpGraphics
            // 
            this.grpGraphics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGraphics.Controls.Add(this.chkSkipSplash);
            this.grpGraphics.Controls.Add(this.cmbSkin);
            this.grpGraphics.Controls.Add(this.label2);
            this.grpGraphics.Controls.Add(this.cmbResolution);
            this.grpGraphics.Controls.Add(this.label1);
            this.grpGraphics.Location = new System.Drawing.Point(12, 35);
            this.grpGraphics.Name = "grpGraphics";
            this.grpGraphics.Size = new System.Drawing.Size(310, 96);
            this.grpGraphics.TabIndex = 1;
            this.grpGraphics.TabStop = false;
            this.grpGraphics.Text = "Graphics";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Startup resolution:";
            // 
            // cmbResolution
            // 
            this.cmbResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Location = new System.Drawing.Point(104, 19);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(200, 21);
            this.cmbResolution.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Skin:";
            // 
            // cmbSkin
            // 
            this.cmbSkin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSkin.FormattingEnabled = true;
            this.cmbSkin.Location = new System.Drawing.Point(104, 46);
            this.cmbSkin.Name = "cmbSkin";
            this.cmbSkin.Size = new System.Drawing.Size(200, 21);
            this.cmbSkin.TabIndex = 3;
            // 
            // chkSkipSplash
            // 
            this.chkSkipSplash.AutoSize = true;
            this.chkSkipSplash.Location = new System.Drawing.Point(6, 73);
            this.chkSkipSplash.Name = "chkSkipSplash";
            this.chkSkipSplash.Size = new System.Drawing.Size(115, 17);
            this.chkSkipSplash.TabIndex = 4;
            this.chkSkipSplash.Text = "Skip splash screen";
            this.chkSkipSplash.UseVisualStyleBackColor = true;
            // 
            // grpObjects
            // 
            this.grpObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpObjects.Controls.Add(this.chkCustomIcons);
            this.grpObjects.Controls.Add(this.chkCustomFonts);
            this.grpObjects.Controls.Add(this.chkCustomBackground);
            this.grpObjects.Location = new System.Drawing.Point(12, 137);
            this.grpObjects.Name = "grpObjects";
            this.grpObjects.Size = new System.Drawing.Size(310, 88);
            this.grpObjects.TabIndex = 2;
            this.grpObjects.TabStop = false;
            this.grpObjects.Text = "Custom objects";
            // 
            // chkCustomBackground
            // 
            this.chkCustomBackground.AutoSize = true;
            this.chkCustomBackground.Location = new System.Drawing.Point(6, 19);
            this.chkCustomBackground.Name = "chkCustomBackground";
            this.chkCustomBackground.Size = new System.Drawing.Size(147, 17);
            this.chkCustomBackground.TabIndex = 0;
            this.chkCustomBackground.Text = "Use custom backgrounds";
            this.chkCustomBackground.UseVisualStyleBackColor = true;
            // 
            // chkCustomFonts
            // 
            this.chkCustomFonts.AutoSize = true;
            this.chkCustomFonts.Location = new System.Drawing.Point(6, 42);
            this.chkCustomFonts.Name = "chkCustomFonts";
            this.chkCustomFonts.Size = new System.Drawing.Size(108, 17);
            this.chkCustomFonts.TabIndex = 1;
            this.chkCustomFonts.Text = "Use custom fonts";
            this.chkCustomFonts.UseVisualStyleBackColor = true;
            // 
            // chkCustomIcons
            // 
            this.chkCustomIcons.AutoSize = true;
            this.chkCustomIcons.Location = new System.Drawing.Point(6, 65);
            this.chkCustomIcons.Name = "chkCustomIcons";
            this.chkCustomIcons.Size = new System.Drawing.Size(110, 17);
            this.chkCustomIcons.TabIndex = 2;
            this.chkCustomIcons.Text = "Use custom icons";
            this.chkCustomIcons.UseVisualStyleBackColor = true;
            // 
            // grpPlugins
            // 
            this.grpPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPlugins.Controls.Add(this.chkAutoloadPlugins);
            this.grpPlugins.Location = new System.Drawing.Point(12, 231);
            this.grpPlugins.Name = "grpPlugins";
            this.grpPlugins.Size = new System.Drawing.Size(310, 145);
            this.grpPlugins.TabIndex = 3;
            this.grpPlugins.TabStop = false;
            this.grpPlugins.Text = "Plugins";
            // 
            // chkAutoloadPlugins
            // 
            this.chkAutoloadPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoloadPlugins.FormattingEnabled = true;
            this.chkAutoloadPlugins.IntegralHeight = false;
            this.chkAutoloadPlugins.Location = new System.Drawing.Point(6, 19);
            this.chkAutoloadPlugins.Name = "chkAutoloadPlugins";
            this.chkAutoloadPlugins.Size = new System.Drawing.Size(298, 120);
            this.chkAutoloadPlugins.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(247, 382);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveExit
            // 
            this.btnSaveExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveExit.Location = new System.Drawing.Point(166, 382);
            this.btnSaveExit.Name = "btnSaveExit";
            this.btnSaveExit.Size = new System.Drawing.Size(75, 23);
            this.btnSaveExit.TabIndex = 5;
            this.btnSaveExit.Text = "Save && Exit";
            this.btnSaveExit.UseVisualStyleBackColor = true;
            this.btnSaveExit.Click += new System.EventHandler(this.btnSaveExit_Click);
            // 
            // btnSaveRun
            // 
            this.btnSaveRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRun.Location = new System.Drawing.Point(85, 382);
            this.btnSaveRun.Name = "btnSaveRun";
            this.btnSaveRun.Size = new System.Drawing.Size(75, 23);
            this.btnSaveRun.TabIndex = 6;
            this.btnSaveRun.Text = "Save && Run";
            this.btnSaveRun.UseVisualStyleBackColor = true;
            this.btnSaveRun.Click += new System.EventHandler(this.btnSaveRun_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 417);
            this.Controls.Add(this.btnSaveRun);
            this.Controls.Add(this.btnSaveExit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpPlugins);
            this.Controls.Add(this.grpObjects);
            this.Controls.Add(this.grpGraphics);
            this.Controls.Add(this.chkInitSteam);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(277, 0);
            this.Name = "frmMain";
            this.Text = "Detox configuration";
            this.grpGraphics.ResumeLayout(false);
            this.grpGraphics.PerformLayout();
            this.grpObjects.ResumeLayout(false);
            this.grpObjects.PerformLayout();
            this.grpPlugins.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkInitSteam;
        private System.Windows.Forms.GroupBox grpGraphics;
        private System.Windows.Forms.CheckBox chkSkipSplash;
        private System.Windows.Forms.ComboBox cmbSkin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpObjects;
        private System.Windows.Forms.CheckBox chkCustomIcons;
        private System.Windows.Forms.CheckBox chkCustomFonts;
        private System.Windows.Forms.CheckBox chkCustomBackground;
        private System.Windows.Forms.GroupBox grpPlugins;
        private System.Windows.Forms.CheckedListBox chkAutoloadPlugins;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveExit;
        private System.Windows.Forms.Button btnSaveRun;
    }
}

