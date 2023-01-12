namespace Realism_Mod_Config_GUI
{
    partial class Main_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.revertButton = new System.Windows.Forms.Button();
            this.procLabel = new System.Windows.Forms.Label();
            this.crankCheck = new System.Windows.Forms.CheckBox();
            this.dispMultLabel = new System.Windows.Forms.Label();
            this.convMultLabel = new System.Windows.Forms.Label();
            this.horzRecMultLabel = new System.Windows.Forms.Label();
            this.vertRecMultLabel = new System.Windows.Forms.Label();
            this.ergoMultiLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.revertLabel = new System.Windows.Forms.Label();
            this.savedLabel = new System.Windows.Forms.Label();
            this.modVerLabel = new System.Windows.Forms.Label();
            this.Preset_Tab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.attachPresetCombo = new System.Windows.Forms.ComboBox();
            this.presetHelpLabel = new System.Windows.Forms.Label();
            this.presetLabel = new System.Windows.Forms.Label();
            this.weapPresetCombo = new System.Windows.Forms.ComboBox();
            this.Recoil_Tab = new System.Windows.Forms.TabPage();
            this.globalRecoilGroupBox = new System.Windows.Forms.GroupBox();
            this.procNumeric = new System.Windows.Forms.NumericUpDown();
            this.globalRecoilModiGroupBox = new System.Windows.Forms.GroupBox();
            this.ergoNumeric = new System.Windows.Forms.NumericUpDown();
            this.dispNumeric = new System.Windows.Forms.NumericUpDown();
            this.convNumeric = new System.Windows.Forms.NumericUpDown();
            this.horzRecNumeric = new System.Windows.Forms.NumericUpDown();
            this.vertRecNumeric = new System.Windows.Forms.NumericUpDown();
            this.warningTextBox = new System.Windows.Forms.RichTextBox();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.Preset_Tab.SuspendLayout();
            this.Recoil_Tab.SuspendLayout();
            this.globalRecoilGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.procNumeric)).BeginInit();
            this.globalRecoilModiGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ergoNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.convNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horzRecNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertRecNumeric)).BeginInit();
            this.mainTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 5000;
            this.toolTip1.AutoPopDelay = 5000000;
            this.toolTip1.InitialDelay = 400;
            this.toolTip1.ReshowDelay = 400;
            // 
            // revertButton
            // 
            this.revertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.revertButton.BackColor = System.Drawing.Color.DarkOrange;
            this.revertButton.Location = new System.Drawing.Point(12, 717);
            this.revertButton.Name = "revertButton";
            this.revertButton.Size = new System.Drawing.Size(89, 27);
            this.revertButton.TabIndex = 7;
            this.revertButton.Text = "Revert";
            this.toolTip1.SetToolTip(this.revertButton, "Reverts all settings to default values. \r\nTHIS WILL SAVE WHEN PRESSEED, CANNOT BE" +
        " UNDONE!");
            this.revertButton.UseVisualStyleBackColor = false;
            this.revertButton.Click += new System.EventHandler(this.revertButton_Click);
            // 
            // procLabel
            // 
            this.procLabel.AutoSize = true;
            this.procLabel.Location = new System.Drawing.Point(6, 58);
            this.procLabel.Name = "procLabel";
            this.procLabel.Size = new System.Drawing.Size(112, 15);
            this.procLabel.TabIndex = 11;
            this.procLabel.Text = "Procedural Intensity";
            this.toolTip1.SetToolTip(this.procLabel, "This Is The Intensity Of All Weapon-Related Procedural Animations. This Includes " +
        "Sway, Aim Inertira And Recoil. The Lower It Is, The Less Recoil, Sway And Aim In" +
        "ertia. Recommended To Leave At 1.");
            // 
            // crankCheck
            // 
            this.crankCheck.AutoSize = true;
            this.crankCheck.ForeColor = System.Drawing.Color.White;
            this.crankCheck.Location = new System.Drawing.Point(6, 33);
            this.crankCheck.Name = "crankCheck";
            this.crankCheck.Size = new System.Drawing.Size(136, 19);
            this.crankCheck.TabIndex = 4;
            this.crankCheck.Text = "Enable \'Recoil Crank\'";
            this.toolTip1.SetToolTip(this.crankCheck, "If Recoil Crank Is Enabled, Recoil Will Go Rearwards Into The Shoulder. If It\'s O" +
        "ff Then Recoil Will Go Forward Like In Unmodded EFT.");
            this.crankCheck.UseVisualStyleBackColor = true;
            this.crankCheck.CheckedChanged += new System.EventHandler(this.crankCheck_CheckedChanged);
            // 
            // dispMultLabel
            // 
            this.dispMultLabel.AutoSize = true;
            this.dispMultLabel.Location = new System.Drawing.Point(6, 120);
            this.dispMultLabel.Name = "dispMultLabel";
            this.dispMultLabel.Size = new System.Drawing.Size(93, 15);
            this.dispMultLabel.TabIndex = 7;
            this.dispMultLabel.Text = "Dispersion Multi";
            this.toolTip1.SetToolTip(this.dispMultLabel, "Dispersion Is Basically The Amount Of Spread. It\'s The Radius In Which Recoil Can" +
        " Occur, So Higher Dispersion = More Spread.");
            // 
            // convMultLabel
            // 
            this.convMultLabel.AutoSize = true;
            this.convMultLabel.Location = new System.Drawing.Point(6, 91);
            this.convMultLabel.Name = "convMultLabel";
            this.convMultLabel.Size = new System.Drawing.Size(108, 15);
            this.convMultLabel.TabIndex = 5;
            this.convMultLabel.Text = "Convergence Multi";
            this.toolTip1.SetToolTip(this.convMultLabel, resources.GetString("convMultLabel.ToolTip"));
            // 
            // horzRecMultLabel
            // 
            this.horzRecMultLabel.AutoSize = true;
            this.horzRecMultLabel.Location = new System.Drawing.Point(6, 62);
            this.horzRecMultLabel.Name = "horzRecMultLabel";
            this.horzRecMultLabel.Size = new System.Drawing.Size(128, 15);
            this.horzRecMultLabel.TabIndex = 3;
            this.horzRecMultLabel.Text = "Horizontal Recoil Multi";
            this.toolTip1.SetToolTip(this.horzRecMultLabel, resources.GetString("horzRecMultLabel.ToolTip"));
            // 
            // vertRecMultLabel
            // 
            this.vertRecMultLabel.AutoSize = true;
            this.vertRecMultLabel.Location = new System.Drawing.Point(6, 33);
            this.vertRecMultLabel.Name = "vertRecMultLabel";
            this.vertRecMultLabel.Size = new System.Drawing.Size(111, 15);
            this.vertRecMultLabel.TabIndex = 1;
            this.vertRecMultLabel.Text = "Vertical Recoil Multi";
            this.toolTip1.SetToolTip(this.vertRecMultLabel, "Vertical Recoil Multi. Higher Vertical Recoil = More Muzzle Rise And Flip.");
            // 
            // ergoMultiLabel
            // 
            this.ergoMultiLabel.AutoSize = true;
            this.ergoMultiLabel.Location = new System.Drawing.Point(6, 149);
            this.ergoMultiLabel.Name = "ergoMultiLabel";
            this.ergoMultiLabel.Size = new System.Drawing.Size(62, 15);
            this.ergoMultiLabel.TabIndex = 9;
            this.ergoMultiLabel.Text = "Ergo Multi";
            this.toolTip1.SetToolTip(this.ergoMultiLabel, "Ergonomics Multi. In This Mod, Higher Ergo = Reduction To Weight And Balance Pena" +
        "lties, So Faster ADS, Less Aim Sway And Inertira, Faster Weapon Handling (Reload" +
        "ing, Chambering).");
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.BackColor = System.Drawing.Color.GreenYellow;
            this.saveButton.Location = new System.Drawing.Point(1226, 717);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(89, 27);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // revertLabel
            // 
            this.revertLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.revertLabel.AutoSize = true;
            this.revertLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.revertLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.revertLabel.Location = new System.Drawing.Point(5, 699);
            this.revertLabel.Name = "revertLabel";
            this.revertLabel.Size = new System.Drawing.Size(101, 15);
            this.revertLabel.TabIndex = 9;
            this.revertLabel.Text = "Settings Reverted!";
            // 
            // savedLabel
            // 
            this.savedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.savedLabel.AutoSize = true;
            this.savedLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.savedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.savedLabel.Location = new System.Drawing.Point(1227, 699);
            this.savedLabel.Name = "savedLabel";
            this.savedLabel.Size = new System.Drawing.Size(86, 15);
            this.savedLabel.TabIndex = 10;
            this.savedLabel.Text = "Settings Saved!";
            // 
            // modVerLabel
            // 
            this.modVerLabel.AutoSize = true;
            this.modVerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.modVerLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.modVerLabel.ForeColor = System.Drawing.Color.White;
            this.modVerLabel.Location = new System.Drawing.Point(934, 477);
            this.modVerLabel.Name = "modVerLabel";
            this.modVerLabel.Size = new System.Drawing.Size(78, 25);
            this.modVerLabel.TabIndex = 11;
            this.modVerLabel.Text = "modVer";
            // 
            // Preset_Tab
            // 
            this.Preset_Tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.Preset_Tab.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Preset_Tab.BackgroundImage")));
            this.Preset_Tab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Preset_Tab.Controls.Add(this.label1);
            this.Preset_Tab.Controls.Add(this.attachPresetCombo);
            this.Preset_Tab.Controls.Add(this.presetHelpLabel);
            this.Preset_Tab.Controls.Add(this.presetLabel);
            this.Preset_Tab.Controls.Add(this.weapPresetCombo);
            this.Preset_Tab.ForeColor = System.Drawing.Color.White;
            this.Preset_Tab.Location = new System.Drawing.Point(4, 24);
            this.Preset_Tab.Name = "Preset_Tab";
            this.Preset_Tab.Size = new System.Drawing.Size(1331, 736);
            this.Preset_Tab.TabIndex = 5;
            this.Preset_Tab.Text = "Presets";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Attatchment Preset:";
            // 
            // attachPresetCombo
            // 
            this.attachPresetCombo.FormattingEnabled = true;
            this.attachPresetCombo.Location = new System.Drawing.Point(133, 99);
            this.attachPresetCombo.Name = "attachPresetCombo";
            this.attachPresetCombo.Size = new System.Drawing.Size(188, 23);
            this.attachPresetCombo.TabIndex = 3;
            this.attachPresetCombo.SelectedIndexChanged += new System.EventHandler(this.attachPresetCombo_SelectedIndexChanged);
            // 
            // presetHelpLabel
            // 
            this.presetHelpLabel.AutoSize = true;
            this.presetHelpLabel.Location = new System.Drawing.Point(13, 27);
            this.presetHelpLabel.Name = "presetHelpLabel";
            this.presetHelpLabel.Size = new System.Drawing.Size(649, 15);
            this.presetHelpLabel.TabIndex = 2;
            this.presetHelpLabel.Text = "Select An Installed Preset From The Dropdown Box Below, And Then Press \'Save\' To " +
    "Load It. Remeber To Restart The Server.";
            // 
            // presetLabel
            // 
            this.presetLabel.AutoSize = true;
            this.presetLabel.Location = new System.Drawing.Point(12, 64);
            this.presetLabel.Name = "presetLabel";
            this.presetLabel.Size = new System.Drawing.Size(89, 15);
            this.presetLabel.TabIndex = 1;
            this.presetLabel.Text = "Weapon Preset:";
            // 
            // weapPresetCombo
            // 
            this.weapPresetCombo.FormattingEnabled = true;
            this.weapPresetCombo.Location = new System.Drawing.Point(133, 61);
            this.weapPresetCombo.Name = "weapPresetCombo";
            this.weapPresetCombo.Size = new System.Drawing.Size(188, 23);
            this.weapPresetCombo.TabIndex = 0;
            this.weapPresetCombo.SelectedIndexChanged += new System.EventHandler(this.weapPresetCombo_SelectedIndexChanged);
            // 
            // Recoil_Tab
            // 
            this.Recoil_Tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.Recoil_Tab.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Recoil_Tab.BackgroundImage")));
            this.Recoil_Tab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Recoil_Tab.Controls.Add(this.globalRecoilGroupBox);
            this.Recoil_Tab.Controls.Add(this.globalRecoilModiGroupBox);
            this.Recoil_Tab.Controls.Add(this.warningTextBox);
            this.Recoil_Tab.Location = new System.Drawing.Point(4, 24);
            this.Recoil_Tab.Margin = new System.Windows.Forms.Padding(0);
            this.Recoil_Tab.Name = "Recoil_Tab";
            this.Recoil_Tab.Padding = new System.Windows.Forms.Padding(3);
            this.Recoil_Tab.Size = new System.Drawing.Size(1331, 736);
            this.Recoil_Tab.TabIndex = 0;
            this.Recoil_Tab.Text = "Recoil";
            // 
            // globalRecoilGroupBox
            // 
            this.globalRecoilGroupBox.Controls.Add(this.procLabel);
            this.globalRecoilGroupBox.Controls.Add(this.crankCheck);
            this.globalRecoilGroupBox.Controls.Add(this.procNumeric);
            this.globalRecoilGroupBox.ForeColor = System.Drawing.Color.White;
            this.globalRecoilGroupBox.Location = new System.Drawing.Point(13, 233);
            this.globalRecoilGroupBox.Name = "globalRecoilGroupBox";
            this.globalRecoilGroupBox.Size = new System.Drawing.Size(320, 106);
            this.globalRecoilGroupBox.TabIndex = 13;
            this.globalRecoilGroupBox.TabStop = false;
            this.globalRecoilGroupBox.Text = "Global Recoil and Weapon Settings";
            // 
            // procNumeric
            // 
            this.procNumeric.DecimalPlaces = 2;
            this.procNumeric.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.procNumeric.Location = new System.Drawing.Point(147, 56);
            this.procNumeric.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.procNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.procNumeric.Name = "procNumeric";
            this.procNumeric.Size = new System.Drawing.Size(120, 23);
            this.procNumeric.TabIndex = 10;
            this.procNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.procNumeric.ValueChanged += new System.EventHandler(this.procNumeric_ValueChanged);
            // 
            // globalRecoilModiGroupBox
            // 
            this.globalRecoilModiGroupBox.Controls.Add(this.ergoMultiLabel);
            this.globalRecoilModiGroupBox.Controls.Add(this.ergoNumeric);
            this.globalRecoilModiGroupBox.Controls.Add(this.dispMultLabel);
            this.globalRecoilModiGroupBox.Controls.Add(this.dispNumeric);
            this.globalRecoilModiGroupBox.Controls.Add(this.convMultLabel);
            this.globalRecoilModiGroupBox.Controls.Add(this.convNumeric);
            this.globalRecoilModiGroupBox.Controls.Add(this.horzRecMultLabel);
            this.globalRecoilModiGroupBox.Controls.Add(this.horzRecNumeric);
            this.globalRecoilModiGroupBox.Controls.Add(this.vertRecMultLabel);
            this.globalRecoilModiGroupBox.Controls.Add(this.vertRecNumeric);
            this.globalRecoilModiGroupBox.ForeColor = System.Drawing.Color.White;
            this.globalRecoilModiGroupBox.Location = new System.Drawing.Point(13, 22);
            this.globalRecoilModiGroupBox.Name = "globalRecoilModiGroupBox";
            this.globalRecoilModiGroupBox.Size = new System.Drawing.Size(320, 191);
            this.globalRecoilModiGroupBox.TabIndex = 12;
            this.globalRecoilModiGroupBox.TabStop = false;
            this.globalRecoilModiGroupBox.Text = "Global Recoil Modifiers";
            // 
            // ergoNumeric
            // 
            this.ergoNumeric.DecimalPlaces = 2;
            this.ergoNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ergoNumeric.Location = new System.Drawing.Point(147, 147);
            this.ergoNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ergoNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ergoNumeric.Name = "ergoNumeric";
            this.ergoNumeric.Size = new System.Drawing.Size(120, 23);
            this.ergoNumeric.TabIndex = 8;
            this.ergoNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ergoNumeric.ValueChanged += new System.EventHandler(this.ergoNumeric_ValueChanged);
            // 
            // dispNumeric
            // 
            this.dispNumeric.DecimalPlaces = 2;
            this.dispNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.dispNumeric.Location = new System.Drawing.Point(147, 118);
            this.dispNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.dispNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.dispNumeric.Name = "dispNumeric";
            this.dispNumeric.Size = new System.Drawing.Size(120, 23);
            this.dispNumeric.TabIndex = 6;
            this.dispNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dispNumeric.ValueChanged += new System.EventHandler(this.dispNumeric_ValueChanged);
            // 
            // convNumeric
            // 
            this.convNumeric.DecimalPlaces = 2;
            this.convNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.convNumeric.Location = new System.Drawing.Point(147, 89);
            this.convNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.convNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.convNumeric.Name = "convNumeric";
            this.convNumeric.Size = new System.Drawing.Size(120, 23);
            this.convNumeric.TabIndex = 4;
            this.convNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.convNumeric.ValueChanged += new System.EventHandler(this.convNumeric_ValueChanged);
            // 
            // horzRecNumeric
            // 
            this.horzRecNumeric.DecimalPlaces = 2;
            this.horzRecNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.horzRecNumeric.Location = new System.Drawing.Point(147, 60);
            this.horzRecNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.horzRecNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.horzRecNumeric.Name = "horzRecNumeric";
            this.horzRecNumeric.Size = new System.Drawing.Size(120, 23);
            this.horzRecNumeric.TabIndex = 2;
            this.horzRecNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.horzRecNumeric.ValueChanged += new System.EventHandler(this.horzRecNumeric_ValueChanged);
            // 
            // vertRecNumeric
            // 
            this.vertRecNumeric.DecimalPlaces = 2;
            this.vertRecNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.vertRecNumeric.Location = new System.Drawing.Point(147, 31);
            this.vertRecNumeric.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.vertRecNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.vertRecNumeric.Name = "vertRecNumeric";
            this.vertRecNumeric.Size = new System.Drawing.Size(120, 23);
            this.vertRecNumeric.TabIndex = 0;
            this.vertRecNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.vertRecNumeric.ValueChanged += new System.EventHandler(this.vertRecNumeric_ValueChanged);
            // 
            // warningTextBox
            // 
            this.warningTextBox.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.warningTextBox.ForeColor = System.Drawing.Color.Red;
            this.warningTextBox.Location = new System.Drawing.Point(385, 206);
            this.warningTextBox.Name = "warningTextBox";
            this.warningTextBox.Size = new System.Drawing.Size(628, 225);
            this.warningTextBox.TabIndex = 11;
            this.warningTextBox.Text = "CONFIG.JSON NOT FOUND! PLEASE ENSURE ALL FILES ARE IN THE CORRECT LOCATION!\n....";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.Recoil_Tab);
            this.mainTabControl.Controls.Add(this.Preset_Tab);
            this.mainTabControl.HotTrack = true;
            this.mainTabControl.Location = new System.Drawing.Point(-5, -2);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.Padding = new System.Drawing.Point(0, 0);
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1339, 764);
            this.mainTabControl.TabIndex = 0;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1327, 755);
            this.Controls.Add(this.savedLabel);
            this.Controls.Add(this.revertLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.revertButton);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.modVerLabel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main_Form";
            this.Text = " ";
            this.Preset_Tab.ResumeLayout(false);
            this.Preset_Tab.PerformLayout();
            this.Recoil_Tab.ResumeLayout(false);
            this.globalRecoilGroupBox.ResumeLayout(false);
            this.globalRecoilGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.procNumeric)).EndInit();
            this.globalRecoilModiGroupBox.ResumeLayout(false);
            this.globalRecoilModiGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ergoNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.convNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horzRecNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vertRecNumeric)).EndInit();
            this.mainTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ToolTip toolTip1;
        private Button revertButton;
        private Button saveButton;
        private Label revertLabel;
        private Label savedLabel;
        private Label modVerLabel;
        private TabPage Preset_Tab;
        private TabPage Recoil_Tab;
        private RichTextBox warningTextBox;
        private TabControl mainTabControl;
        private GroupBox globalRecoilModiGroupBox;
        private CheckBox crankCheck;
        private Label dispMultLabel;
        private NumericUpDown dispNumeric;
        private Label convMultLabel;
        private NumericUpDown convNumeric;
        private Label horzRecMultLabel;
        private NumericUpDown horzRecNumeric;
        private Label vertRecMultLabel;
        private NumericUpDown vertRecNumeric;
        private GroupBox globalRecoilGroupBox;
        private Label ergoMultiLabel;
        private NumericUpDown ergoNumeric;
        private Label procLabel;
        private NumericUpDown procNumeric;
        private Label presetLabel;
        private ComboBox weapPresetCombo;
        private Label presetHelpLabel;
        private Label label1;
        private ComboBox attachPresetCombo;
    }
}