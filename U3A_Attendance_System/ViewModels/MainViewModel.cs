using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;
using U3A_Attendance_System.Views;
using System.Dynamic;
using System.Windows;

namespace U3A_Attendance_System.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        //private WindowManager _wm = new WindowManager();
        protected Facade _facade = FacadeFactory.Instance();
        private TabbedViewModel tabbedView = new TabbedViewModel();

        public MainViewModel()
        {
            ShowTabbedView();
            this.DisplayName = "U3A Attendance";
        }

        //TODO: For some reason it cant distinquish between paramater types...
        public void ShowCIEdit(Object obj)
        {
            if(obj is ICourseDescription)
                ActivateItem(new CourseInstanceEditViewModel((ICourseDescription)obj));
            else
                ActivateItem(new CourseInstanceEditViewModel((ICourseInstance)obj));
        }

        public void ShowCIList(ICourseDescription cd)
        {
            ActivateItem(new CourseInstanceListViewModel(cd));
            //tabbedView.SelectedTab = 2;
        }

        public void ShowCISessionEdit(ISession session)
        {
            new CourseInstanceSessionEditViewModel(session);
        }

        public void DeleteDescriptionConfirm(ICourseDescription cd)
        {
            if (MessageBox.Show("Are you sure you want to delete the Course Descripton: '" + cd.Title + "' ?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _facade.DeleteCourseDescription(cd.Id);

        }

        public void ShowTabbedView()
        {
            ActivateItem(tabbedView);
        }

        public void ShowHome()
        {
            ShowTabbedView();
        }

    }
}
