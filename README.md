# MonitorSwitch

# Description

A Windows application which switches a monitor input source via DDC protocol when the arrival of a specific USB device is detected, and optionally when the PC power is resumed.

The application is based on:

https://github.com/adamtuliper/WindowsDeviceNotifications (DeviceNotification.cs)

https://github.com/valleyman86/AutoKVM (DisplayDDC.cs)

# Installation

Copy the files from the Release folder to a folder on your PC and run MonitorSwitch.exe

A log file will appear in a folder called 'logs' under the folder you created on your PC.

# Configuration

1. USB Device

The path of the USB device, e.g. \\?\USB#VID_2CCF&PID_0854#7&2ce20b0d&0&4#{a5dcbf10-6530-11d2-901f-00c04fb951ed}
https://www.nirsoft.net/utils/usb_devices_view.html can help identify the device you want to monitor

In addition, if you:

- Enable 'Log USB Device Arrival'
- Click 'Start'
- Attach the USB device you wish to monitor (or detach and attach it) then the path of the device will be written to the log file (see the logs folder). You can then copy this path to the USB Device text box.

2. The monitor index (i.e. which physical monitor to switch) and the monitor channel (i.e. the input channel to switch to) 

https://www.nirsoft.net/utils/control_my_monitor.html can help determine these. Input select is parameter 60 and the tool shows the possible values
so you can experiment to determine the correct channel. For a single monitor, the monitor index will be 0.

When you have configured the settings, click Start to begin monitoring. You can minimise the application and it will continue to run. You can open the form again to manage the settings by clicking the icon in the system tray.

# Other settings

'Run on startup enabled' means the application will load and run automatically (minimized to the start tray) when the PC boots.

'Switch on power resume' means that the monitor will be switched when the PC power resumes, in addition to when the monitored USB device arrives.


# Registry

Registry values are stored in SOFTWARE\Plummet\MonitorSwitch
