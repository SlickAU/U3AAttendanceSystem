using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model.Interfaces;

namespace U3A_Attendance_Model
{[Serializable]
    internal partial class Location : ILocation, ISearchable
    {
        
        internal Location(Guid venueId, string room)
        {
            VenueId = venueId;
            Room = room;
        }

        internal Location update(string room, Guid venueId)
        {
            Room = room;
            VenueId = venueId;
            return this;
        }

        internal void delete(Action<Location> action)
        {
            action(this);
        }




        public bool MeetsCritera(string keyword)
        {
            if (this.Room.ToLower().Trim().Contains(keyword))
            {
                return true; 
            }
            return false; 
        }


        IVenue ILocation.Venue
        {
            get { return Venue; }
        }
    }
}
