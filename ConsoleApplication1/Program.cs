using System;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.ComponentModel;
using System.Configuration.Install;

namespace SimpleServiceCs
{
    public class SimpleService : ServiceBase
    {
        static void Main()
        {
            // Set the SystemEvents class to receive event notification when a user  
            // preference changes, the palette changes, or when display settings change.
            SystemEvents.UserPreferenceChanging += new
                UserPreferenceChangingEventHandler(SystemEvents_UserPreferenceChanging);
            SystemEvents.PaletteChanged += new
                EventHandler(SystemEvents_PaletteChanged);
            SystemEvents.DisplaySettingsChanged += new
                EventHandler(SystemEvents_DisplaySettingsChanged);

            // For demonstration purposes, this application sits idle waiting for events.
            Console.WriteLine("This application is waiting for system events.");
            Console.WriteLine("Press <Enter> to terminate this application.");
            Console.ReadLine();
        }

        // This method is called when a user preference changes. 
        static void SystemEvents_UserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
        {
            Console.WriteLine("The user preference is changing. Category={0}", e.Category);
        }

        // This method is called when the palette changes. 
        static void SystemEvents_PaletteChanged(object sender, EventArgs e)
        {
            Console.WriteLine("The palette changed.");
        }

        // This method is called when the display settings change. 
        static void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            Console.WriteLine("The display settings changed.");
        }
    }


}
