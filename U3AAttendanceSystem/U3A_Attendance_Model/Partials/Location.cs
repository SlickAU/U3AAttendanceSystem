using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    internal partial class Location : ILocation
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

    }
}
