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
        #region Fields

        public IEnumerable<IVenue> Venues { get; set; }
        public IEnumerable<ILocation> Locations { get; set; }

        public IVenue SelectedVenue { get; set; }
        public ILocation SelectedLocation { get; set; }
        public DateTime SelectedDate { get; set; }

        public ISession CurrentSession { get; set; } 

        #endregion

        //Constructor
        public CourseInstanceSessionEditViewModel(ISession session)
        {
            SelectedDate = session.Date.Date;
            SelectedVenue = session.Location.Venue;
            SelectedLocation = session.Location;
            CurrentSession = session;

            Venues = _facade.FetchAllVenues().Where(v => v.RegionId.Equals(session.Location.Venue.RegionId));
            Locations = session.Location.Venue.Locations;
            _wm.ShowDialog(this, null, settings);
        }

        public void UpdateLocations()
        {
            Locations = SelectedVenue.Locations;
            this.Refresh();
        }

        public void Save()
        {
            var a = _facade.UpdateSession(CurrentSession.Id, SelectedLocation.Id, SelectedDate, CurrentSession.VisitorCount, CurrentSession.CourseInstanceId, SelectedVenue.RegionId);
            this.Refresh();
        }
    }
}
