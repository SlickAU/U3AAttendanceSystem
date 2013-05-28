using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using U3A_Attendance_System.ViewModels;
using System.Windows.Threading;
using log4net;

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
            logger.Error(DateTime.Now + " - " + e.Exception.InnerException.Message);

            //Handle Exceptions to allow for resume on non-critical failure of program
            e.Handled = true;
        }
    }
}

