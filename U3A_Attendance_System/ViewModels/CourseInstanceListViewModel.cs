using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class CourseInstanceListViewModel : BaseViewModel
    {
        private List<ICourseInstance> ciList = new List<ICourseInstance>();

        public BindingList<ICourseInstance> InstancesList
        {
            get
            {
                return new BindingList<ICourseInstance>(ciList);
            }
            set
            {
                ciList = value.ToList();
                NotifyOfPropertyChange("InstancesList");
            }
        }

        public CourseInstanceListViewModel()
        {
            InstancesList = new BindingList<ICourseInstance>(_facade.AllInstances().ToList());
        }

        public CourseInstanceListViewModel(ICourseDescription cd)
        {
            InstancesList = new BindingList<ICourseInstance>(cd.CourseInstances.ToList());
        }

        public void RefreshList()
        {
            InstancesList = new BindingList<ICourseInstance>(_facade.AllInstances().ToList());
        }

        public void FetchInstances(ICourseDescription cd)
        {
            InstancesList = new BindingList<ICourseInstance>(cd.CourseInstances.ToList());
        }

        public void ShowCIDelete(ICourseInstance ci)
        {
            settings.Title = "Delete Course Instance";
            _wm.ShowDialog(new DeleteViewModel((ICourseInstance)ci, this), null, settings);
            NotifyOfPropertyChange("InstancesList");
        }
    }
}
