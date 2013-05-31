using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using U3A_Attendance_System.ViewModels;
using System.Windows.Threading;
using log4net;
using U3A_Attendance_Model;
using System.Windows;

namespace U3A_Attendance_System
{
    public class U3ABootStrapper : Bootstrapper<MainViewModel>
    {
        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(U3ABootStrapper));

        public U3ABootStrapper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {

            if (e.Exception.InnerException != null)
            {
                logger.Error(DateTime.Now + " - " + e.Exception.InnerException.Message);
            }
            else
            {
                logger.Error(DateTime.Now + " - " + e.Exception.Message);
            }

            if (e.Exception.InnerException is BusinessRuleException)
            {
                MessageBox.Show(e.Exception.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("Whoops! Something went wrong.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }
    }
}

