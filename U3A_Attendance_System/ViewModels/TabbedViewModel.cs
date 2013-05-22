using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_System.ViewModels
{
    public class TabbedViewModel
    {
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


    }
}
