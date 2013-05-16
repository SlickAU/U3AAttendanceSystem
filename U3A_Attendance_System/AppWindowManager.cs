using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace U3A_Attendance_System
{
    public class AppWindowManager : WindowManager
    {
        public AppWindowManager()
        {
            
        }

        protected override Window CreateWindow(object rootModel, bool isDialog, object context, IDictionary<string, object> settings)
        {
            isDialog = true;
            Window window = base.CreateWindow(rootModel, isDialog, context, settings);

            return window;
        }


    }
}
