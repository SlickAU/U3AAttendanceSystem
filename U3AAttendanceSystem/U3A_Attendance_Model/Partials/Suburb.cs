using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    internal partial class Suburb : ISuburb
    {

        #region Venue Management

        internal Venue createVenue(string name, string address, string codeId)
        {
            var venue = new Venue(name, address, codeId, this.Id);
            this.Venues.Add(venue);
            return venue;
        }

        internal Venue updateVenue(Guid venueId, string name, string address, string codeId)
        {
            var venue = fetchVenue(venueId);
            return venue.update(name, address, codeId);
        }

        internal Venue fetchVenue(Guid venueId)
        {
            var result = Venues.Where(v => v.Id.Equals(venueId)).FirstOrDefault();

            if (result == null)
            {
                throw new BusinessRuleException("Invalid Venue identifier supplied");
            }

            return result;
        }

        internal IEnumerable<Venue> fetchVenues()
        {
            var result = Venues.AsEnumerable();

            if (result == null)
            {
                throw new BusinessRuleException("Could not obtain collection of Venues");
            }

            return result;
        }

        internal void deleteVenue(Guid venueId, Action<Venue> action)
        {
            fetchVenue(venueId).delete(action);
        }

        #endregion

        #region Location Management

        internal Location createLocation(Guid venueId, string room)
        {
            return fetchVenue(venueId).createLocation(room);
        }
               
        internal Location fetchLocation(Guid venueId, Guid locationId)
        {
            return fetchVenue(venueId).fetchLocation(locationId);
        }

        internal IEnumerable<Location> fetchLocations(Guid venueId)
        {
            return fetchVenue(venueId).fetchLocations();
        }

        internal Location updateLocation(Guid venueId, Guid locationId, string room)
        {
            return fetchVenue(venueId).updateLocation(locationId, room);
        }

        internal void deleteLocation(Guid venueId, Guid locationId, Action<Location> action)
        {
            fetchVenue(venueId).deleteLocation(locationId, action);
        }

        #endregion
    }
}
