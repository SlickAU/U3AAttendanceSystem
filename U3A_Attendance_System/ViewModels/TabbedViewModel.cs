using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class TabbedViewModel : BaseViewModel
    {
        private int selectedTab = 0;

        public int SelectedTab
        {
            get
            {
                return selectedTab;
            }
            set
            {
                selectedTab = value;
                NotifyOfPropertyChange("SelectedTab");
            }
        }
        public CourseDescriptionListViewModel View1 { get; set; }
        public VenueListViewModel View2 { get; set; }
        public CourseInstanceListViewModel View3 { get; set; }
        public CoordinatorListViewModel View4 { get; set; }
        public TabbedViewModel()
        {
            View1 = new CourseDescriptionListViewModel();
            View2 = new VenueListViewModel();
            View3 = new CourseInstanceListViewModel();
            View4 = new CoordinatorListViewModel();
        }

        public void ShowCIList(ICourseDescription cd)
        {
            View3 = new CourseInstanceListViewModel(cd);
            this.Refresh();
            //View3.PopulateInstancesList(cd.CourseInstances);
            SelectedTab = 2;
        }


    }
}
