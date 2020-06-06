namespace Speedtest
{
    partial class BBLTest
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
            this.buttonRunTest = new System.Windows.Forms.Button();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.labelInterval = new System.Windows.Forms.Label();
            this.labelIntervalUnit = new System.Windows.Forms.Label();
            this.buttonStartDaemon = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.labelSavePath = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.checkBoxAutoTest = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRunTest
            // 
            this.buttonRunTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRunTest.Location = new System.Drawing.Point(12, 167);
            this.buttonRunTest.Name = "buttonRunTest";
            this.buttonRunTest.Size = new System.Drawing.Size(285, 23);
            this.buttonRunTest.TabIndex = 0;
            this.buttonRunTest.Text = "Run single test";
            this.buttonRunTest.UseVisualStyleBackColor = true;
            this.buttonRunTest.Click += new System.EventHandler(this.buttonRunTest_Click);
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownInterval.Location = new System.Drawing.Point(108, 18);
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(112, 22);
            this.numericUpDownInterval.TabIndex = 1;
            this.numericUpDownInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownInterval.ValueChanged += new System.EventHandler(this.numericUpDownInterval_ValueChanged);
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(12, 20);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(54, 17);
            this.labelInterval.TabIndex = 2;
            this.labelInterval.Text = "Interval";
            // 
            // labelIntervalUnit
            // 
            this.labelIntervalUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIntervalUnit.AutoSize = true;
            this.labelIntervalUnit.Location = new System.Drawing.Point(226, 20);
            this.labelIntervalUnit.Name = "labelIntervalUnit";
            this.labelIntervalUnit.Size = new System.Drawing.Size(57, 17);
            this.labelIntervalUnit.TabIndex = 3;
            this.labelIntervalUnit.Text = "minutes";
            // 
            // buttonStartDaemon
            // 
            this.buttonStartDaemon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartDaemon.Location = new System.Drawing.Point(12, 138);
            this.buttonStartDaemon.Name = "buttonStartDaemon";
            this.buttonStartDaemon.Size = new System.Drawing.Size(285, 23);
            this.buttonStartDaemon.TabIndex = 4;
            this.buttonStartDaemon.Text = "Start Daemon";
            this.buttonStartDaemon.UseVisualStyleBackColor = true;
            this.buttonStartDaemon.Click += new System.EventHandler(this.buttonStartDaemon_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Location = new System.Drawing.Point(12, 196);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(285, 23);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Double-click to open the application";
            this.notifyIcon.BalloonTipTitle = "Speedtest";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Text = "Speedtest";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSavePath.Location = new System.Drawing.Point(108, 46);
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(112, 22);
            this.textBoxSavePath.TabIndex = 6;
            this.textBoxSavePath.TextChanged += new System.EventHandler(this.textBoxSavePath_TextChanged);
            // 
            // labelSavePath
            // 
            this.labelSavePath.AutoSize = true;
            this.labelSavePath.Location = new System.Drawing.Point(12, 49);
            this.labelSavePath.Name = "labelSavePath";
            this.labelSavePath.Size = new System.Drawing.Size(72, 17);
            this.labelSavePath.TabIndex = 7;
            this.labelSavePath.Text = "Save path";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(226, 46);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(71, 23);
            this.buttonBrowse.TabIndex = 8;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // checkBoxAutoTest
            // 
            this.checkBoxAutoTest.AutoSize = true;
            this.checkBoxAutoTest.Location = new System.Drawing.Point(15, 74);
            this.checkBoxAutoTest.Name = "checkBoxAutoTest";
            this.checkBoxAutoTest.Size = new System.Drawing.Size(231, 21);
            this.checkBoxAutoTest.TabIndex = 9;
            this.checkBoxAutoTest.Text = "Auto-test upon starting software";
            this.checkBoxAutoTest.UseVisualStyleBackColor = true;
            this.checkBoxAutoTest.CheckedChanged += new System.EventHandler(this.checkBoxAutoTest_CheckedChanged);
            // 
            // checkBoxAutoStart
            // 
            this.checkBoxAutoStart.AutoSize = true;
            this.checkBoxAutoStart.Location = new System.Drawing.Point(15, 101);
            this.checkBoxAutoStart.Name = "checkBoxAutoStart";
            this.checkBoxAutoStart.Size = new System.Drawing.Size(176, 21);
            this.checkBoxAutoStart.TabIndex = 10;
            this.checkBoxAutoStart.Text = "Auto-start with windows";
            this.checkBoxAutoStart.UseVisualStyleBackColor = true;
            this.checkBoxAutoStart.CheckedChanged += new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(115, 52);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // BBLTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 233);
            this.Controls.Add(this.checkBoxAutoStart);
            this.Controls.Add(this.checkBoxAutoTest);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelSavePath);
            this.Controls.Add(this.textBoxSavePath);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStartDaemon);
            this.Controls.Add(this.labelIntervalUnit);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.numericUpDownInterval);
            this.Controls.Add(this.buttonRunTest);
            this.MinimumSize = new System.Drawing.Size(330, 280);
            this.Name = "BBLTest";
            this.ShowInTaskbar = false;
            this.Text = "Speedtest";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BBLTest_FormClosing);
            this.Resize += new System.EventHandler(this.BBLTest_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRunTest;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.Label labelIntervalUnit;
        private System.Windows.Forms.Button buttonStartDaemon;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.Label labelSavePath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.CheckBox checkBoxAutoTest;
        private System.Windows.Forms.CheckBox checkBoxAutoStart;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

