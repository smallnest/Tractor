namespace Kuaff.Tractor
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.GameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PauseGametoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CardImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CommonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OperaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomCardImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CardBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BlueWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GreenAgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AntelopeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomBackImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CardDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KuaffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.BackMusicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoBackMusicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayMusicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RandomPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.SetRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetGameFinishedtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.RobotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAlgorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SeeTotalScoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameToolStripMenuItem,
            this.ConfigToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(629, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // GameToolStripMenuItem
            // 
            this.GameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartToolStripMenuItem,
            this.PauseGametoolStripMenuItem,
            this.toolStripSeparator2,
            this.SaveToolStripMenuItem,
            this.RestoreToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.GameToolStripMenuItem.Name = "GameToolStripMenuItem";
            this.GameToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.GameToolStripMenuItem.Text = "游戏";
            // 
            // StartToolStripMenuItem
            // 
            this.StartToolStripMenuItem.Image = global::Kuaff.Tractor.Properties.Resources.MenuStart;
            this.StartToolStripMenuItem.Name = "StartToolStripMenuItem";
            this.StartToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.StartToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.StartToolStripMenuItem.Text = "开始新游戏";
            this.StartToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // PauseGametoolStripMenuItem
            // 
            this.PauseGametoolStripMenuItem.Image = global::Kuaff.Tractor.Properties.Resources.MenuPause;
            this.PauseGametoolStripMenuItem.Name = "PauseGametoolStripMenuItem";
            this.PauseGametoolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.PauseGametoolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.PauseGametoolStripMenuItem.Text = "暂停游戏";
            this.PauseGametoolStripMenuItem.Click += new System.EventHandler(this.PauseGametoolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Image = global::Kuaff.Tractor.Properties.Resources.MenuSave;
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.SaveToolStripMenuItem.Text = "保存牌局";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // RestoreToolStripMenuItem
            // 
            this.RestoreToolStripMenuItem.Image = global::Kuaff.Tractor.Properties.Resources.MenuOpen;
            this.RestoreToolStripMenuItem.Name = "RestoreToolStripMenuItem";
            this.RestoreToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.RestoreToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.RestoreToolStripMenuItem.Text = "读取牌局";
            this.RestoreToolStripMenuItem.Click += new System.EventHandler(this.RestoreToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameSpeedToolStripMenuItem,
            this.toolStripSeparator3,
            this.CardImageToolStripMenuItem,
            this.CardBackToolStripMenuItem,
            this.CardDesktopToolStripMenuItem,
            this.toolStripSeparator4,
            this.BackMusicToolStripMenuItem,
            this.toolStripSeparator7,
            this.SetRulesToolStripMenuItem,
            this.SetGameFinishedtoolStripMenuItem,
            this.toolStripSeparator6,
            this.RobotToolStripMenuItem,
            this.toolStripSeparator8,
            this.SelectAlgorithmToolStripMenuItem});
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.ConfigToolStripMenuItem.Text = "设置";
            // 
            // GameSpeedToolStripMenuItem
            // 
            this.GameSpeedToolStripMenuItem.Image = global::Kuaff.Tractor.Properties.Resources.MenuSpeed;
            this.GameSpeedToolStripMenuItem.Name = "GameSpeedToolStripMenuItem";
            this.GameSpeedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.GameSpeedToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.GameSpeedToolStripMenuItem.Text = "游戏速度";
            this.GameSpeedToolStripMenuItem.Click += new System.EventHandler(this.GameSpeedToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // CardImageToolStripMenuItem
            // 
            this.CardImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CommonToolStripMenuItem,
            this.ModelToolStripMenuItem,
            this.OperaToolStripMenuItem,
            this.CustomCardImageToolStripMenuItem});
            this.CardImageToolStripMenuItem.Name = "CardImageToolStripMenuItem";
            this.CardImageToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.CardImageToolStripMenuItem.Text = "牌面图案";
            // 
            // CommonToolStripMenuItem
            // 
            this.CommonToolStripMenuItem.Checked = true;
            this.CommonToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CommonToolStripMenuItem.Name = "CommonToolStripMenuItem";
            this.CommonToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.CommonToolStripMenuItem.Text = "普通图案";
            this.CommonToolStripMenuItem.Click += new System.EventHandler(this.SelectCardImage_Click);
            // 
            // ModelToolStripMenuItem
            // 
            this.ModelToolStripMenuItem.Name = "ModelToolStripMenuItem";
            this.ModelToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.ModelToolStripMenuItem.Text = "香车美女";
            this.ModelToolStripMenuItem.Click += new System.EventHandler(this.SelectCardImage_Click);
            // 
            // OperaToolStripMenuItem
            // 
            this.OperaToolStripMenuItem.Name = "OperaToolStripMenuItem";
            this.OperaToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.OperaToolStripMenuItem.Text = "京剧脸谱";
            this.OperaToolStripMenuItem.Click += new System.EventHandler(this.SelectCardImage_Click);
            // 
            // CustomCardImageToolStripMenuItem
            // 
            this.CustomCardImageToolStripMenuItem.Name = "CustomCardImageToolStripMenuItem";
            this.CustomCardImageToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.CustomCardImageToolStripMenuItem.Text = "自定义";
            this.CustomCardImageToolStripMenuItem.Click += new System.EventHandler(this.SelectCardImage_Click);
            // 
            // CardBackToolStripMenuItem
            // 
            this.CardBackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BlueWorldToolStripMenuItem,
            this.GreenAgeToolStripMenuItem,
            this.AntelopeToolStripMenuItem,
            this.CustomBackImageToolStripMenuItem});
            this.CardBackToolStripMenuItem.Name = "CardBackToolStripMenuItem";
            this.CardBackToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.CardBackToolStripMenuItem.Text = "牌背图案";
            // 
            // BlueWorldToolStripMenuItem
            // 
            this.BlueWorldToolStripMenuItem.Checked = true;
            this.BlueWorldToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BlueWorldToolStripMenuItem.Name = "BlueWorldToolStripMenuItem";
            this.BlueWorldToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.BlueWorldToolStripMenuItem.Text = "蔚蓝世界";
            this.BlueWorldToolStripMenuItem.Click += new System.EventHandler(this.SelectBackImage_Click);
            // 
            // GreenAgeToolStripMenuItem
            // 
            this.GreenAgeToolStripMenuItem.Name = "GreenAgeToolStripMenuItem";
            this.GreenAgeToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.GreenAgeToolStripMenuItem.Text = "青涩年华";
            this.GreenAgeToolStripMenuItem.Click += new System.EventHandler(this.SelectBackImage_Click);
            // 
            // AntelopeToolStripMenuItem
            // 
            this.AntelopeToolStripMenuItem.Name = "AntelopeToolStripMenuItem";
            this.AntelopeToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.AntelopeToolStripMenuItem.Text = "草原羚羊";
            this.AntelopeToolStripMenuItem.Click += new System.EventHandler(this.SelectBackImage_Click);
            // 
            // CustomBackImageToolStripMenuItem
            // 
            this.CustomBackImageToolStripMenuItem.Name = "CustomBackImageToolStripMenuItem";
            this.CustomBackImageToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.CustomBackImageToolStripMenuItem.Text = "自定义";
            this.CustomBackImageToolStripMenuItem.Click += new System.EventHandler(this.SelectBackImage_Click);
            // 
            // CardDesktopToolStripMenuItem
            // 
            this.CardDesktopToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KuaffToolStripMenuItem,
            this.SelectImageToolStripMenuItem});
            this.CardDesktopToolStripMenuItem.Name = "CardDesktopToolStripMenuItem";
            this.CardDesktopToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.CardDesktopToolStripMenuItem.Text = "牌桌图案";
            // 
            // KuaffToolStripMenuItem
            // 
            this.KuaffToolStripMenuItem.Checked = true;
            this.KuaffToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KuaffToolStripMenuItem.Name = "KuaffToolStripMenuItem";
            this.KuaffToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.KuaffToolStripMenuItem.Text = "夸父科技";
            this.KuaffToolStripMenuItem.Click += new System.EventHandler(this.SelectImage_Click);
            // 
            // SelectImageToolStripMenuItem
            // 
            this.SelectImageToolStripMenuItem.Name = "SelectImageToolStripMenuItem";
            this.SelectImageToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.SelectImageToolStripMenuItem.Text = "自定义图片";
            this.SelectImageToolStripMenuItem.Click += new System.EventHandler(this.SelectImage_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(157, 6);
            // 
            // BackMusicToolStripMenuItem
            // 
            this.BackMusicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NoBackMusicToolStripMenuItem,
            this.PlayMusicToolStripMenuItem,
            this.RandomPlayToolStripMenuItem});
            this.BackMusicToolStripMenuItem.Image = global::Kuaff.Tractor.Properties.Resources.music;
            this.BackMusicToolStripMenuItem.Name = "BackMusicToolStripMenuItem";
            this.BackMusicToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.BackMusicToolStripMenuItem.Text = "背景音乐";
            // 
            // NoBackMusicToolStripMenuItem
            // 
            this.NoBackMusicToolStripMenuItem.Checked = true;
            this.NoBackMusicToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NoBackMusicToolStripMenuItem.Name = "NoBackMusicToolStripMenuItem";
            this.NoBackMusicToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.NoBackMusicToolStripMenuItem.Text = "无背景音乐";
            this.NoBackMusicToolStripMenuItem.Click += new System.EventHandler(this.NoBackMusicToolStripMenuItem_Click_1);
            // 
            // PlayMusicToolStripMenuItem
            // 
            this.PlayMusicToolStripMenuItem.Name = "PlayMusicToolStripMenuItem";
            this.PlayMusicToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PlayMusicToolStripMenuItem.Text = "播放背景音乐";
            this.PlayMusicToolStripMenuItem.Click += new System.EventHandler(this.PlayMusicToolStripMenuItem_Click);
            // 
            // RandomPlayToolStripMenuItem
            // 
            this.RandomPlayToolStripMenuItem.Name = "RandomPlayToolStripMenuItem";
            this.RandomPlayToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.RandomPlayToolStripMenuItem.Text = "随机播放";
            this.RandomPlayToolStripMenuItem.Click += new System.EventHandler(this.RandomPlayToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(157, 6);
            // 
            // SetRulesToolStripMenuItem
            // 
            this.SetRulesToolStripMenuItem.Name = "SetRulesToolStripMenuItem";
            this.SetRulesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.SetRulesToolStripMenuItem.Text = "游戏规则";
            this.SetRulesToolStripMenuItem.Click += new System.EventHandler(this.SetRulesToolStripMenuItem_Click);
            // 
            // SetGameFinishedtoolStripMenuItem
            // 
            this.SetGameFinishedtoolStripMenuItem.Name = "SetGameFinishedtoolStripMenuItem";
            this.SetGameFinishedtoolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.SetGameFinishedtoolStripMenuItem.Text = "游戏结束轮数";
            this.SetGameFinishedtoolStripMenuItem.Click += new System.EventHandler(this.SetGameFinishedtoolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(157, 6);
            // 
            // RobotToolStripMenuItem
            // 
            this.RobotToolStripMenuItem.CheckOnClick = true;
            this.RobotToolStripMenuItem.Image = global::Kuaff.Tractor.Properties.Resources.robot;
            this.RobotToolStripMenuItem.Name = "RobotToolStripMenuItem";
            this.RobotToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.RobotToolStripMenuItem.Text = "机器人罗伯特";
            this.RobotToolStripMenuItem.Click += new System.EventHandler(this.RobotToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(157, 6);
            // 
            // SelectAlgorithmToolStripMenuItem
            // 
            this.SelectAlgorithmToolStripMenuItem.Name = "SelectAlgorithmToolStripMenuItem";
            this.SelectAlgorithmToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.SelectAlgorithmToolStripMenuItem.Text = "算法插件";
            this.SelectAlgorithmToolStripMenuItem.Click += new System.EventHandler(this.SelectAlgorithmToolStripMenuItem_Click);
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FereToolStripMenuItem,
            this.SeeTotalScoresToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.ToolsToolStripMenuItem.Text = "工具";
            // 
            // FereToolStripMenuItem
            // 
            this.FereToolStripMenuItem.Image = global::Kuaff.Tractor.Properties.Resources.fere;
            this.FereToolStripMenuItem.Name = "FereToolStripMenuItem";
            this.FereToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.FereToolStripMenuItem.Text = "拖拉机伴侣";
            this.FereToolStripMenuItem.Click += new System.EventHandler(this.FereToolStripMenuItem_Click);
            // 
            // SeeTotalScoresToolStripMenuItem
            // 
            this.SeeTotalScoresToolStripMenuItem.Name = "SeeTotalScoresToolStripMenuItem";
            this.SeeTotalScoresToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.SeeTotalScoresToolStripMenuItem.Text = "计分牌";
            this.SeeTotalScoresToolStripMenuItem.Click += new System.EventHandler(this.SeeTotalScoresToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameHelpToolStripMenuItem,
            this.toolStripSeparator5,
            this.AboutMeToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.HelpToolStripMenuItem.Text = "帮助";
            // 
            // GameHelpToolStripMenuItem
            // 
            this.GameHelpToolStripMenuItem.Image = global::Kuaff.Tractor.Properties.Resources.MenuHelp;
            this.GameHelpToolStripMenuItem.Name = "GameHelpToolStripMenuItem";
            this.GameHelpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.GameHelpToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.GameHelpToolStripMenuItem.Text = "游戏帮助";
            this.GameHelpToolStripMenuItem.Click += new System.EventHandler(this.GameHelpToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(138, 6);
            // 
            // AboutMeToolStripMenuItem
            // 
            this.AboutMeToolStripMenuItem.Name = "AboutMeToolStripMenuItem";
            this.AboutMeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.AboutMeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.AboutMeToolStripMenuItem.Text = "关于";
            this.AboutMeToolStripMenuItem.Click += new System.EventHandler(this.AboutMeToolStripMenuItem_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "png图片|*.PNG|jpg图片|*.JPG|bmp图片|*.BMP|gif图片|*.GIF|所有文件|*.*";
            this.openFileDialog.Title = "选择背景图片";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "拖拉机大战";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Kuaff.Tractor.Properties.Resources.Backgroud;
            this.ClientSize = new System.Drawing.Size(629, 470);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拖拉机大战";
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDoubleClick);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem GameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RestoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        internal System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem GameSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CardImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CommonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OperaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CardBackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BlueWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GreenAgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AntelopeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PauseGametoolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem CardDesktopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GameHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SelectImageToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        internal System.Windows.Forms.ToolStripMenuItem KuaffToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem RobotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BackMusicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NoBackMusicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayMusicToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem SetRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RandomPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CustomCardImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CustomBackImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SeeTotalScoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem SelectAlgorithmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetGameFinishedtoolStripMenuItem;
    }
}

