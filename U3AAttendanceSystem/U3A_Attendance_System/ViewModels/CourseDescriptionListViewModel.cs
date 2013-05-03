using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using U3A_Attendance_Model;
using U3A_Attendance_System.Views;

namespace U3A_Attendance_System.ViewModels
{
    public class CourseDescriptionListViewModel : BaseViewModel
    {
        #region Properties

        public IEnumerable<ICourseDescription> CourseDescriptions
        {
            get { return _facade.FetchCourseDescriptions(); }
        }

        #endregion

        #region Commands/Behaviours

        public void DisplayCDManager(ICourseDescription cd)
        {
            
        }

        public void RollMe()
        {
           //_facade.Wtf();
            this.Refresh();
        }

        //bool CanDisplayCDManagerExecute()
        //{
        //    return true;
        //}

        #endregion
    }
}
