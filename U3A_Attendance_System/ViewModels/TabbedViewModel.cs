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
        public CourseInstanceListViewModel View2 { get; set; }
        public VenueListViewModel View3 { get; set; }
        public CoordinatorListViewModel View4 { get; set; }


        public TabbedViewModel(MainViewModel main)
        {
            View1 = new CourseDescriptionListViewModel(main, this);
            View2 = new CourseInstanceListViewModel();
            View3 = new VenueListViewModel();
            View4 = new CoordinatorListViewModel();
        }

        public void ResetView()
        {

        }

        public void ShowCIList()
        {
            View2 = new CourseInstanceListViewModel();
            this.Refresh();
        }
    }
}
