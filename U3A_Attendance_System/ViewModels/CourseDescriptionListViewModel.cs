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
                    courseDescriptions = _facade.FetchCourseDescriptions();
                
                return courseDescriptions;
            }

            set { courseDescriptions = value; NotifyOfPropertyChange("CourseDescriptions"); }
        }

        public string TitleSearch { get { return titleSearch; } set { titleSearch = value; SearchTitles(); } }

        #endregion

        #region Commands/Behaviours

        public void ShowCDEdit(Object cd)
        {
            if (cd is ICourseDescription)
            {
                settings.Title = "Edit course description";
                //settings.SizeToContent = SizeToContent.Manual;

                _wm.ShowDialog(new CourseDescriptionEditViewModel((ICourseDescription)cd), null, settings);
                NotifyOfPropertyChange("CourseDescriptions");
                this.Refresh();
            }
            else
            {
                settings.Title = "Create course description";
                //settings.SizeToContent = SizeToContent.Manual;


                _wm.ShowDialog(new CourseDescriptionEditViewModel(), null, settings);
            }
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
