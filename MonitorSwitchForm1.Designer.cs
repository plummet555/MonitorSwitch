namespace MonitorSwitch
{
    partial class MonitorSwitchForm1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorSwitchForm1));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.textBox_usbDevice = new System.Windows.Forms.TextBox();
            this.numericUpDown_index = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_source = new System.Windows.Forms.NumericUpDown();
            this.button_main = new System.Windows.Forms.Button();
            this.checkBox_autoStart = new System.Windows.Forms.CheckBox();
            this.checkBox_switchOnResume = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_index)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_source)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Tip text";
            this.notifyIcon.BalloonTipTitle = "Tip title";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Monitor Switch";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "USB Device";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Monitor Index";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Monitor Source";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(12, 113);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(39, 15);
            this.label_status.TabIndex = 6;
            this.label_status.Text = "Status";
            // 
            // textBox_usbDevice
            // 
            this.textBox_usbDevice.Location = new System.Drawing.Point(118, 6);
            this.textBox_usbDevice.Name = "textBox_usbDevice";
            this.textBox_usbDevice.Size = new System.Drawing.Size(505, 23);
            this.textBox_usbDevice.TabIndex = 7;
            this.textBox_usbDevice.Text = "USB";
            // 
            // numericUpDown_index
            // 
            this.numericUpDown_index.Location = new System.Drawing.Point(118, 40);
            this.numericUpDown_index.Name = "numericUpDown_index";
            this.numericUpDown_index.Size = new System.Drawing.Size(79, 23);
            this.numericUpDown_index.TabIndex = 8;
            // 
            // numericUpDown_source
            // 
            this.numericUpDown_source.Location = new System.Drawing.Point(118, 73);
            this.numericUpDown_source.Name = "numericUpDown_source";
            this.numericUpDown_source.Size = new System.Drawing.Size(79, 23);
            this.numericUpDown_source.TabIndex = 9;
            // 
            // button_main
            // 
            this.button_main.Location = new System.Drawing.Point(12, 143);
            this.button_main.Name = "button_main";
            this.button_main.Size = new System.Drawing.Size(81, 32);
            this.button_main.TabIndex = 10;
            this.button_main.Text = "Start";
            this.button_main.UseVisualStyleBackColor = true;
            this.button_main.Click += new System.EventHandler(this.button_main_Click);
            // 
            // checkBox_autoStart
            // 
            this.checkBox_autoStart.AutoSize = true;
            this.checkBox_autoStart.Location = new System.Drawing.Point(118, 155);
            this.checkBox_autoStart.Name = "checkBox_autoStart";
            this.checkBox_autoStart.Size = new System.Drawing.Size(104, 19);
            this.checkBox_autoStart.TabIndex = 11;
            this.checkBox_autoStart.Text = "Run on startup";
            this.checkBox_autoStart.UseVisualStyleBackColor = true;
            this.checkBox_autoStart.CheckedChanged += new System.EventHandler(this.checkBox_autoStart_CheckedChanged);
            // 
            // checkBox_switchOnResume
            // 
            this.checkBox_switchOnResume.AutoSize = true;
            this.checkBox_switchOnResume.Location = new System.Drawing.Point(243, 156);
            this.checkBox_switchOnResume.Name = "checkBox_switchOnResume";
            this.checkBox_switchOnResume.Size = new System.Drawing.Size(156, 19);
            this.checkBox_switchOnResume.TabIndex = 12;
            this.checkBox_switchOnResume.Text = "Switch on power resume";
            this.checkBox_switchOnResume.UseVisualStyleBackColor = true;
            this.checkBox_switchOnResume.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // MonitorSwitchForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 186);
            this.Controls.Add(this.checkBox_switchOnResume);
            this.Controls.Add(this.checkBox_autoStart);
            this.Controls.Add(this.button_main);
            this.Controls.Add(this.numericUpDown_source);
            this.Controls.Add(this.numericUpDown_index);
            this.Controls.Add(this.textBox_usbDevice);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MonitorSwitchForm1";
            this.Text = "Monitor Switch";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_index)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_source)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NotifyIcon notifyIcon;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label_status;
        private TextBox textBox_usbDevice;
        private NumericUpDown numericUpDown_index;
        private NumericUpDown numericUpDown_source;
        private Button button_main;
        private CheckBox checkBox_autoStart;
        private CheckBox checkBox_switchOnResume;
    }
}