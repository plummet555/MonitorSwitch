using Serilog;
using System.Threading;

namespace MonitorSwitch
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>

        private static Mutex mutex = null;
        private static string appName = "MonitorSwitch";
        
        [STAThread]
        static void Main(string[] args)
        {
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                //app is already running! Exiting the application
                return;
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MonitorSwitchForm1(args));
        }

    }
}