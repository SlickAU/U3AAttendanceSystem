using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    internal partial class Venue : IVenue 
    {

        public Venue(string name, string address, string codeId, Guid suburbId)
        {
            Name = name;
            Address = address;
            CodeId = codeId;
            SuburbId = suburbId;
            Locations = new List<Location>();
        }

        internal Venue update(string name, string address, string codeId)
        {
            Name = name;
            Address = address;
            CodeId = codeId;
            return this;
        }

        internal void delete(Action<Venue> action)
        {
            action(this);
        }

        #region Location Management

        internal Location createLocation(string room)
        {
            var location = new Location(this.Id, room);
            this.Locations.Add(location);
            return location;
        }
        
        internal Location fetchLocation(Guid locationId)
        {
            var result = this.Locations.FirstOrDefault(l => l.Id.Equals(locationId));

            if (result == null)
            {
                throw new BusinessRuleException("Invalid location identifier supplied");
            }

            return result;
        }

        internal IEnumerable<Location> fetchLocations()
        {
            var result = Locations.AsEnumerable();

            if (result == null)
            {
                throw new BusinessRuleException("Could not obtain collection of Locations");
            }

            return result;
        }

        internal Location updateLocation(Guid locationId, string room)
        {
            return fetchLocation(locationId).update(room, this.Id);
        }

        internal void deleteLocation(Guid locationId, Action<Location> action)
        {
            fetchLocation(locationId).delete(action);
        }

        #endregion


        IEnumerable<ILocation> IVenue.Locations
        {
            get { return Locations; }
        }


        public Guid RegionId
        {
            get { return Suburb.RegionId; }
        }
    }
}
