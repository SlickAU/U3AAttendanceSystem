using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class WarningViewModel : BaseViewModel
    {
        public string Label1 { get; set; }
        public string Message { get; set; }
        object itemToDelete;
        
        public WarningViewModel(ICourseInstance instance)
        {
            Label1 = instance.CourseDescription.Title;
            Message = "Warning! The selected Course Instance\n has associated sessions.\n\nAre you sure you want to delete those sessions aswell?"; 
            itemToDelete = instance;
        }
        
        public WarningViewModel(IVenue venue)
        {
            Label1 = venue.Name;
            Message = "Warning! The selected Venue\n has associated locations.\n\nAre you sure you want to delete those locations aswell?"; 
            itemToDelete = venue;
        }

        public WarningViewModel(ISession session)
        {
            Label1 = session.Date.ToString();
            Message = "Warning! The selected Session\n has associated attendances.\n\nAre you sure you want to delete the attendances aswell?";
            itemToDelete = session;
        }

        public void Delete()
        {
            if (itemToDelete is IVenue)
            {
                IVenue venue = (IVenue)itemToDelete;
                
                var locations = venue.Locations.AsEnumerable();
                int count = locations.Count();

                for (int i = 0; i < count; i++)
                {
                    _facade.DeleteLocation(venue.RegionId, venue.SuburbId, venue.Id, locations.ElementAt(0).Id);
                }
               
                _facade.DeleteVenue(venue.Id, venue.RegionId, venue.SuburbId);
            }

            if (itemToDelete is ICourseInstance)
            {
                ICourseInstance instance = (ICourseInstance)itemToDelete;

                var sessions = instance.Sessions.AsEnumerable();
                int count = sessions.Count();

                for (int i = 0; i < count; i++)
                {
                    _facade.DeleteSession(sessions.ElementAt(0).Id, instance.Id, instance.RegionId);
                }
                
                _facade.DeleteCourseInstance(instance.Id, instance.RegionId);
            }

            if (itemToDelete is ISession)
            {
                ISession session = (ISession)itemToDelete;

                int attCount = session.Attendances.Count();

                for (int i = 0; i < attCount; i++)
                {
                    _facade.DeleteAttendance(session.CourseInstance.RegionId, session.CourseInstanceId, session.Id, session.Attendances.ElementAt(0).Id);
                }

                _facade.DeleteSession(session.Id, session.CourseInstanceId, session.CourseInstance.RegionId);

            }
        }

    }
}
