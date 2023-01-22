# MonitorSwitch

Based on:

https://github.com/adamtuliper/WindowsDeviceNotifications (DeviceNotification.cs)

https://github.com/valleyman86/AutoKVM (DisplayDDC.cs)

# Usage

Switches a monitor input source via DDC protocol when the arrival of a specific USB device is detected, and optionally when the PC power is resumed.

Configure:

1. The path of the USB device, e.g. \\?\USB#VID_2CCF&PID_0854#7&2ce20b0d&0&4#{a5dcbf10-6530-11d2-901f-00c04fb951ed}
https://www.nirsoft.net/utils/usb_devices_view.html can help identify the device you want to monitor
This area needs work as it isn't easy to find a path in the format that currently has to be specified.

2. The monitor index (i.e. which physical monitor to switch) and the monitor channel (i.e. the input channel to switch to) 

https://www.nirsoft.net/utils/control_my_monitor.html can help determine these. Input select is parameter 60 and the tool shows the possible values
so you can experiment to determine the correct channel. For a single monitor, the monitor index will be 0.

Run on startup enabled means the application will load and run automatically (minimized to the start tray) when the PC boots.

Switch on power resume means that the monitor will be switched when the PC power resumes, in addition to when the monitored USB device arrives.

#Event log

Key events are written to event log source MonitorSwitch

#Registry

Registry values are stored in SOFTWARE\Plummet\MonitorSwitch