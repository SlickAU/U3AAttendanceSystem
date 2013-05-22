using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;
using U3A_Attendance_System.Views;

namespace U3A_Attendance_System.ViewModels
{

    public class CourseInstanceSessionEditViewModel : BaseViewModel
    {
        public string SessionDate { get; set; }
        public IEnumerable<IVenue> Venues { get; set; }
        public IVenue Venue { get; set; }
        public IEnumerable<ILocation> Locations { get; set; }
        public ILocation Location { get; set; }
        public ISession currentSession { get; set; }

        public DateTime SelectedDate { get; set; }
        public IVenue SelectedVenue { get; set; }
        public ILocation SelectedLocation { get; set; }

        public CourseInstanceSessionEditViewModel(ISession session)
        {
            SelectedDate = Convert.ToDateTime(session.Date.ToShortDateString());
            Venues = _facade.FetchAllVenues().Where(v => v.RegionId.Equals(session.Location.Venue.RegionId));
            Venue = session.Location.Venue;
            Locations = Venue.Locations;
            Location = session.Location;
        }

        public void Save()
        {
            _facade.UpdateSession(currentSession.Id, SelectedLocation.Id, SelectedDate, currentSession.VisitorCount, currentSession.CourseInstanceId, SelectedVenue.RegionId);
        }
    }
}
