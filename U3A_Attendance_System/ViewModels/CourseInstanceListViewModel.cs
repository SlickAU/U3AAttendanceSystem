using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class CourseInstanceListViewModel : BaseViewModel
    {
        private ICourseDescription cd;
        private ObservableCollection<ICourseInstance> ciList;
 
        public ObservableCollection<ICourseInstance> InstancesList
        {
            get
            {
              // return new ObservableCollection<ICourseInstance>(_facade.FetchCourseInstancesByDescription(cd.Id));
                return ciList;
            }
            set
            {
                ciList = value;
                NotifyOfPropertyChange("InstancesList");
            }
        }

        public CourseInstanceListViewModel()
        {
            InstancesList = new ObservableCollection<ICourseInstance>(_facade.AllInstances().ToList());
        }

        public void RefreshList()
        {
            InstancesList = new ObservableCollection<ICourseInstance>(_facade.AllInstances().ToList());
        }

        public void FetchCDInstances(ICourseDescription cd)
        {
            this.cd = cd;
            InstancesList = new ObservableCollection<ICourseInstance>(_facade.FetchCourseInstancesByDescription(cd.Id));
        }

        public void ShowCIDelete(ICourseInstance ci)
        {
            settings.Title = "Delete Course Instance";
            _wm.ShowDialog(new DeleteViewModel((ICourseInstance)ci, this), null, settings);
            NotifyOfPropertyChange("InstancesList");
            this.Refresh();
        }
    }
}
