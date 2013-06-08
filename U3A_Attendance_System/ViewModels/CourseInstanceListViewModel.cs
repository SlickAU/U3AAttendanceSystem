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
        public CourseDescriptionListViewModel cdm;

        public ObservableCollection<ICourseInstance> InstancesList
        {
            get
            {
                var b = ciList;
                  return ciList;
            }
            set
            {
                ciList = value;
            }
        }

        //Constructor
        public CourseInstanceListViewModel()
        {
            InstancesList = new ObservableCollection<ICourseInstance>(_facade.AllInstances().ToList());
        }

        //Manual Refresh of All Instances List
        public void RefreshList()
        {
            InstancesList = new ObservableCollection<ICourseInstance>(_facade.AllInstances().ToList());
            NotifyOfPropertyChange("InstancesList");
        }

        //Fetches all CourseDescription relevant instances
        public void FetchCDInstances(ICourseDescription cd, CourseDescriptionListViewModel cdm)
        {
            this.cd = cd;
            this.cdm = cdm;
            InstancesList = new ObservableCollection<ICourseInstance>(_facade.FetchCourseInstancesByDescription(cd.Id));
            NotifyOfPropertyChange("InstancesList");
        }

        public void ShowCIDelete(ICourseInstance ci)
        {
            settings.Title = "Delete Course Instance";
            _wm.ShowDialog(new DeleteViewModel((ICourseInstance)ci, this), null, settings);
            InstancesList = new ObservableCollection<ICourseInstance>(cd.CourseInstances.ToList());
            NotifyOfPropertyChange("InstancesList");
            cdm.Refresh();
        }

       
    }
}
