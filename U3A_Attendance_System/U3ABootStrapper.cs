using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using U3A_Attendance_System.ViewModels;
using System.Windows.Threading;
using log4net;
using Microsoft.Win32;

namespace U3A_Attendance_System
{
    public class U3ABootStrapper : Bootstrapper<MainViewModel>
    {
        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(U3ABootStrapper));

        public U3ABootStrapper()
        {
            log4net.Config.XmlConfigurator.Configure();
            // Set the SystemEvents class to receive event notification when a user  
            // preference changes, the palette changes, or when display settings change.
            SystemEvents.UserPreferenceChanging += new UserPreferenceChangingEventHandler(SystemEvents_UserPreferenceChanging);
            SystemEvents.PaletteChanged += new EventHandler(SystemEvents_PaletteChanged);
            SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Error(DateTime.Now + " - " + e.Exception.InnerException.Message);
            //Handle Exceptions to allow for resume on non-critical failure of program
            e.Handled = true;
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

