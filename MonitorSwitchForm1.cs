using Microsoft.Win32;
using MonitorSwitchService;
using Serilog;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Linq;
using static MonitorSwitchService.DisplayDDC;

namespace MonitorSwitch
{
    public partial class MonitorSwitchForm1 : Form
    {
        private const String EVENT_LOG_SOURCE = "MonitorSwitch";
        private const String EVENT_LOG_NAME = "MonitorSwitch";
        private const String LOG_FILE_PATH = @".\logs\MonitorSwitch.txt";
        private const String APPLICATION_NAME = @"MonitorSwitch";
        private const String MINIMIZED_FLAG = @"-minimized";

        private const String REG_KEY = @"SOFTWARE\Plummet\MonitorSwitch";
        private const String REG_DEV_PATH = "UsbDevice";
        private const String REG_MONITOR_SOURCE = "MonitorSource";
        private const String REG_MONITOR_INDEX = "MonitorIndex";
        private const String REG_SWITCH_ON_RESUME = "SwitchOnResume";

        private String usbDevicePath = "";
        private UInt32 monitorSource;
        private UInt32 monitorIndex;
        private bool switchOnResume;
        private int state;

        private enum STATE : int
        {
            STOPPED,
            RUNNING,
            ERROR
        }

        public MonitorSwitchForm1(string[] args)
        {
            InitializeComponent();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Trace()
                .WriteTo.File(LOG_FILE_PATH, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Application starting");

            notifyIcon.Visible = false;
            state = (int)STATE.STOPPED;

            if ((args.Length > 0) && (args[0].Equals(MINIMIZED_FLAG)))
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }

            checkBox_autoStart.Checked = CheckStartupEnabled();

            readRegistry();
            startMonitoring();           

        }

        protected void readRegistry()
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(REG_KEY);

            if (key == null)
            {
                label_status.Text = "No registry subkey";
                Log.Information("No registry subkey");
                return;
            }

            String? usbDevicePathChk = (String?)key.GetValue(REG_DEV_PATH, null);

            if (usbDevicePathChk == null)
            {
                label_status.Text = "No USB device path string in registry";
                Log.Information("No USB device path string in registry");
                return;
            }

            usbDevicePath = (String)usbDevicePathChk;

            var valSource = key.GetValue(REG_MONITOR_SOURCE, null);
            if (valSource == null)
            {
                label_status.Text = "No monitor source value in registry";
                Log.Information("No monitor source value in registry");
                return;
            }

            monitorSource = Convert.ToUInt32(valSource);

            var valIndex = key.GetValue(REG_MONITOR_INDEX, null);
            if (valIndex == null)
            {
                label_status.Text = "No monitor index value in registry";
                Log.Information("No monitor index value in registry");
                return;
            }

            monitorIndex = Convert.ToUInt32(valIndex);

            var valSwitchOnResume = key.GetValue(REG_SWITCH_ON_RESUME, null);
            if (valSwitchOnResume == null)
            {
                switchOnResume = false;
            }
            else
            {
                switchOnResume = Convert.ToBoolean(valSwitchOnResume);
            }

            textBox_usbDevice.Text = usbDevicePath;
            numericUpDown_index.Value = monitorIndex;
            numericUpDown_source.Value = monitorSource;
            checkBox_switchOnResume.Checked = switchOnResume;
        }

        protected void writeRegistryUpdateMembers()
        {
            usbDevicePath = textBox_usbDevice.Text;
            monitorIndex = (UInt32)numericUpDown_index.Value;
            monitorSource = (UInt32)numericUpDown_source.Value;
            switchOnResume = checkBox_switchOnResume.Checked;

            RegistryKey? key = Registry.CurrentUser.CreateSubKey(REG_KEY);

            if (key == null)
            {
                label_status.Text = "No registry subkey";
                Log.Information("No registry subkey");
                return;
            }

            key.SetValue(REG_DEV_PATH, usbDevicePath);
            key.SetValue(REG_MONITOR_INDEX, monitorIndex);
            key.SetValue(REG_MONITOR_SOURCE, monitorSource);
            key.SetValue(REG_SWITCH_ON_RESUME, switchOnResume);

        }

        protected void startMonitoring ()
        {
            writeRegistryUpdateMembers();
            if (usbDevicePath == null || usbDevicePath.Length < 1)
            {
                return;
            }

            DeviceNotification.RegisterUsbDeviceNotification(this.Handle);
            SystemEvents.PowerModeChanged += OnPowerChange;

            label_status.Text = "Monitoring";
            state = (int)STATE.RUNNING;
            button_main.Text = "Stop";
            textBox_usbDevice.Enabled = false;
            numericUpDown_index.Enabled = false;
            numericUpDown_source.Enabled = false;
            checkBox_autoStart.Enabled = false;
            checkBox_switchOnResume.Enabled = false;
            checkBox_logUSBDeviceArrival.Enabled = false;
        }

        protected void stopMonitoring ()
        {
            DeviceNotification.UnRegisterUsbDeviceNotification();
            SystemEvents.PowerModeChanged -= OnPowerChange;

            label_status.Text = "Stopped";
            state = (int)STATE.STOPPED;
            button_main.Text = "Start";
            textBox_usbDevice.Enabled = true;
            numericUpDown_index.Enabled = true;
            numericUpDown_source.Enabled = true;
            checkBox_autoStart.Enabled = true;
            checkBox_switchOnResume.Enabled = true;
            checkBox_logUSBDeviceArrival.Enabled = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }

            base.OnLoad(e);
        }
        protected override void OnResize(EventArgs e)
        {                   
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }

            base.OnResize(e);

        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == DeviceNotification.WmDeviceChange)
            {
                switch ((int)m.WParam)
                {
                    case DeviceNotification.DbtDeviceArrival:
                        String name;

                        if (DeviceNotification.IsUsbDevice(m.LParam, out name))
                        {
                            if (checkBox_logUSBDeviceArrival.Checked)
                            {
                                Log.Information("Device arrival: " + name);
                            }
                            if (name.Equals(usbDevicePath))
                            {
                                SwitchMonitor();
                            }
                        }

                        break;
                }
            }
        }

        private void SwitchMonitor()
        {
            Log.Information("Switching monitor " + monitorIndex + " to source " + monitorSource);
            int error;
            DisplayDDC.Init();
            if (DisplayDDC.SetDisplaySource((int)monitorIndex, (int)monitorSource, out error))
            {
                Log.Information("Switched monitor " + monitorIndex + " to source " + monitorSource);
            }
            else
            {
                Log.Information("Error switching monitor " + monitorIndex + " to source " + monitorSource + " , code " + error);
            }
            DisplayDDC.Cleanup();
        }

        private bool CheckStartupEnabled()
        {
            RegistryKey? registryKeyVar = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (registryKeyVar == null)
                return false;

            RegistryKey registryKey = registryKeyVar;

            String? startupPathCheck = (String?)registryKey.GetValue(APPLICATION_NAME, null);

            if (startupPathCheck == null)
            {
                return false;
            }

            String startupPath = (String)startupPathCheck;
            return (startupPath.Equals(Application.ExecutablePath + " " + MINIMIZED_FLAG));

        }
        private void RegisterInStartup(bool isChecked)
        {
            RegistryKey? registryKeyVar = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (registryKeyVar == null)
                return;

            RegistryKey registryKey = registryKeyVar;

            if (isChecked)
            {
                registryKey.SetValue(APPLICATION_NAME, Application.ExecutablePath + @" -minimized");
            }
            else
            {
                registryKey.DeleteValue(APPLICATION_NAME, false);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (state == (int)STATE.RUNNING)
            {
                if (MessageBox.Show("Are you sure you to stop monitoring and exit?", "Monitor Switch", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            if (!e.Cancel)
            {
                Log.Information("Monitor switch application exiting");
                writeRegistryUpdateMembers();
                stopMonitoring();
                notifyIcon.Dispose();
                Log.CloseAndFlush();
            }

            base.OnClosing(e);
        }

        
        private void OnPowerChange(object s, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    Log.Information("Power resume, switch on resume: " + switchOnResume);

                    if (switchOnResume)
                    {
                        SwitchMonitor();
                    }
                    break;
                case PowerModes.Suspend:
                    break;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void button_main_Click(object sender, EventArgs e)
        {
            switch (state)
            {
                case (int) STATE.STOPPED:
                {
                    startMonitoring();
                    break;
                }

                case (int)STATE.RUNNING:
                {
                    stopMonitoring();
                    break;
                }
            }
        }

        private void checkBox_autoStart_CheckedChanged(object sender, EventArgs e)
        {
            RegisterInStartup(checkBox_autoStart.Checked);
        }

        private void checkBox1_switchOnResume_CheckedChanged(object sender, EventArgs e)
        {
            writeRegistryUpdateMembers();
        }

    }
}



