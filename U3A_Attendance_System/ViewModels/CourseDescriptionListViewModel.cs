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
        private IEnumerable<ICourseDescription> courseDescriptions;

        private string titleSearch;
        
        #region Properties

        public IEnumerable<ICourseDescription> CourseDescriptions
        {
            get
            {
                if (courseDescriptions == null)
                    return _facade.FetchCourseDescriptions();
                else
                    return courseDescriptions;
            }

            set { courseDescriptions = value; }
        }

        public string TitleSearch { get { return titleSearch; } set { titleSearch = value; SearchTitles(); } }

        #endregion

        #region Commands/Behaviours

        public void DisplayCDManager(ICourseDescription cd)
        {
            
        }

        public void SearchTitles()
        {
            if (TitleSearch != null)
            {
                string search = TitleSearch.ToUpper();
                CourseDescriptions = _facade.FetchCourseDescriptions().Where(cd => cd.Title.Contains(search));
                this.Refresh();
            }
        }

        public void RollMe()
        {
            this.Refresh();
        }

        #endregion
    }
}
