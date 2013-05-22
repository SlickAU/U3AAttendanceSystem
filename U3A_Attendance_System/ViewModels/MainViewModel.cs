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

        public MainViewModel()
        {
            ShowTabbedView();
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
            ActivateItem(new CourseDescriptionInstancesViewModel(cd));
        }

        public void DeleteDescriptionConfirm(ICourseDescription cd)
        {
            if (MessageBox.Show("Really delete?", "Confirm delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _facade.DeleteCourseDescription(cd.Id);

        }

        public void ShowTabbedView()
        {
            ActivateItem(new TabbedViewModel());
        }

    }
}
