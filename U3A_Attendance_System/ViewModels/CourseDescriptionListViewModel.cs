using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                return _facade.FetchCourseDescriptions();
            }
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
            }
            else
            {
                settings.Title = "Create course description";
                //settings.SizeToContent = SizeToContent.Manual;

                _wm.ShowDialog(new CourseDescriptionEditViewModel(), null, settings);
                NotifyOfPropertyChange("CourseDescriptions");
            }
        }

        public void DeleteDescriptionConfirm(ICourseDescription cd)
        {
            if (MessageBox.Show("Are you sure you want to delete the Course Descripton: '" + cd.Title + "' ?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _facade.DeleteCourseDescription(cd.Id);

            NotifyOfPropertyChange("CourseDescriptions");

        }

        public void SearchTitles()
        {
            if (TitleSearch != null)
            {
                string search = TitleSearch.ToUpper();
                //CourseDescriptions = _facade.FetchCourseDescriptions().Where(cd => cd.Title.Contains(search));
                this.Refresh();
            }
        }

        #endregion
    }
}
