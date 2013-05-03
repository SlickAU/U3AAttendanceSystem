using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model.Partials;


namespace U3A_Attendance_Model
{
    internal partial class U3A
    {
        #region Course Description Management
        
        //Creates CourseDescription
        internal CourseDescription createCourseDescription(string title, string description)
        {
            var courseDescription = new CourseDescription(title, description, this.Id);
            this.CourseDescriptions.Add(courseDescription);
            return courseDescription;
        } 

        //Updates a CourseDescription
        internal CourseDescription updateCourseDescription(Guid courseDescriptionId, string title, string description)
        {
            return fetchCourseDescription(courseDescriptionId).update(title, description);
        }
        
        //Fetches CourseDescription
        internal CourseDescription fetchCourseDescription(Guid courseDescriptionId)
        {
            var result = CourseDescriptions.Where(c => c.Id.Equals(courseDescriptionId)).SingleOrDefault();
            
            if (result == null)
            {
                throw new BusinessRuleException("Invalid Course Description identifier supplied");
            }

            return result;
        }

        //Fetches all CourseDescriptions
        internal IEnumerable<CourseDescription> fetchCourseDescriptions()
        {
            var result = CourseDescriptions.AsEnumerable();

            if (result == null)
            {
                throw new BusinessRuleException("Could not obtain collection of Course Descriptions");
            }

            return result;
        }

        //Delete a CourseDescription
        internal void deleteCourseDescription(Guid courseDescriptionId, Action<CourseDescription> action)
        {
            fetchCourseDescription(courseDescriptionId).delete(action);
        }

        #endregion

        #region Course Instance Management

        internal CourseInstance createCourseInstance(Guid courseDescriptionId, Guid regionId, Guid coordinatorId,Guid suburbId,Guid venueId, Guid defaultLocationId, DateTime startDate, string courseCode)
        {
            var description = fetchCourseDescription(courseDescriptionId);
            var coordinator = fetchCoordinator(coordinatorId); 
            return fetchRegion(regionId).createCourseInstance(description, coordinator,suburbId,venueId, defaultLocationId, startDate, courseCode);
        }

        internal CourseInstance updateCourseInstance(Guid courseInstanceId, Guid coordinatorId, Guid regionId, Guid suburbId, Guid venueId, Guid defaultLocationId, DateTime startDate, int stateId, string courseCode)
        {
            var coordinator = fetchCoordinator(coordinatorId);
            return fetchRegion(regionId).updateCourseInstance(courseInstanceId, coordinator, suburbId, venueId, defaultLocationId, startDate, stateId, courseCode);
        }
        
        internal CourseInstance fetchCourseInstance(Guid courseInstanceId, Guid regionId)
        {
            return fetchRegion(regionId).fetchCourseInstance(courseInstanceId);
        }

        internal IEnumerable<CourseInstance> fetchCourseInstances(Guid regionId)
        {
            return fetchRegion(regionId).fetchCourseInstances();
        }

        internal IEnumerable<CourseInstance> fetchCourseInstancesByDescription(Guid descriptionId)
        {
            return fetchCourseDescription(descriptionId).fetchCourseInstances().AsEnumerable();
        }

        internal void deleteCourseInstance(Guid courseInstanceId, Guid regionId, Action<CourseInstance> action)
        {
            var region = fetchRegion(regionId);
            region.deleteCourseInstance(courseInstanceId, action);       
        }
        
        #endregion

        #region Session Management

        internal Session createSession(Guid courseInstanceId, Guid regionId, Guid suburbId, Guid venueId, Guid locationId, DateTime date)
        {
            return fetchRegion(regionId).createSession(courseInstanceId, suburbId, venueId, locationId, date);
        }

        internal Session updateSession(Guid sessionId, Guid locationId, DateTime date, int visitorCount, Guid courseInstanceId, Guid regionId)
        {
            return fetchRegion(regionId).updateSession(sessionId, locationId, date, visitorCount, courseInstanceId);
        }

        internal ISession fetchSession(Guid sessionId, Guid courseInstanceId, Guid regionId)
        {
            return fetchRegion(regionId).fetchSession(sessionId, courseInstanceId);
        }

        internal IEnumerable<Session> fetchSessions(Guid courseInstanceId, Guid regionId)
        {
            return fetchRegion(regionId).fetchSessions(courseInstanceId);
        }

        internal void deleteSession(Guid sessionId, Guid courseInstanceId, Guid regionId, Action<Session> action)
        {
            fetchRegion(regionId).deleteSession(sessionId, courseInstanceId, action);
        }

        #endregion
       
        #region Region Management

        internal Region fetchRegion(Guid regionid)
        {
            var result =  Regions.Where(r => r.Id.Equals(regionid)).FirstOrDefault();
            
            if (result == null)
            {
                throw new BusinessRuleException("Invalid Region identifier supplied"); 
            }
            
            return result; 
        }

        internal IEnumerable<Region> fetchRegions()
        {
            return Regions;
        }

        #endregion

        #region Suburb Management

        internal IEnumerable<ISuburb> fetchSuburbs(Guid regionId)
        {
            return fetchRegion(regionId).fetchSuburbs();
        }

        #endregion

        #region Venue Management

        internal Venue createVenue(Guid regionId, Guid suburbId, string name, string address, string codeId)
        {
            return fetchRegion(regionId).createVenue(suburbId, name, address, codeId);
        }

        internal Venue updateVenue(Guid venueId, Guid regionId, Guid suburbId, string name, string address, string codeId)
        {
            return fetchRegion(regionId).updateVenue(venueId, suburbId, name, address, codeId);
        }

        internal Venue fetchVenue(Guid venueId, Guid regionId, Guid suburbId)
        {
            return fetchRegion(regionId).fetchVenue(venueId, suburbId);
        }

        internal IEnumerable<Venue> fetchVenues(Guid regionId, Guid suburbId)
        {
            return fetchRegion(regionId).fetchVenues(suburbId);
        }
        internal IEnumerable<Venue> allVenues()
        {
            List<Venue> listOfVenues = new List<Venue>();

            foreach (var r in Regions)
            {
                foreach (var s in r.Suburbs)
                {
                    foreach (var v in s.Venues)
                    {
                        listOfVenues.Add(v);
                    }
                }
            }

            return listOfVenues;
        }

        internal void deleteVenue(Guid venueId, Guid regionId, Guid suburbId, Action<Venue> action)
        {
            fetchRegion(regionId).deleteVenue(venueId, suburbId, action);
        }

        #endregion
       
        #region Coordinator Management

        internal Coordinator createCoordinator(string name, string email, string phoneNumber)
        {
            var coordinator = new Coordinator(name, email, phoneNumber, this.Id);
            this.Coordinators.Add(coordinator);
            return coordinator;
        }

        internal Coordinator updateCoordinator(Guid coordinatorId, string name, string email, string phoneNumber)
        {
            var coordinator = fetchCoordinator(coordinatorId);
            return coordinator.update(name, email, phoneNumber);
        }

        internal Coordinator fetchCoordinator(Guid coordinatorId)
        {
            var result = Coordinators.Where(c => c.Id.Equals(coordinatorId)).FirstOrDefault();

            if (result == null)
            {
                throw new BusinessRuleException("Invalid Coordinator identifier supplied");
            }

            return result;
        }

        internal void deleteCoordinator(Guid coordinatorId, Action<Coordinator> action)
        {
            fetchCoordinator(coordinatorId).delete(action);
        }

        #endregion

        #region Location Management
        
        internal Location createLocation(Guid regionId, Guid suburbId, Guid venueId, string room)
        {
            return fetchRegion(regionId).createLocation(suburbId, venueId, room);
        }

        internal Location fetchLocation(Guid regionId, Guid suburbId, Guid venueId, Guid locationId)
        {
            return fetchRegion(regionId).fetchLocation(venueId, suburbId, locationId);
        }

        internal IEnumerable<Location> fetchLocations(Guid regionId, Guid suburbId, Guid venueId)
        {
            return fetchRegion(regionId).fetchLocations(suburbId, venueId);
        }

        internal Location updateLocation(Guid regionId, Guid suburbId, Guid venueId, Guid locationId, string room)
        {
            return fetchRegion(regionId).updateLocation(suburbId, venueId, locationId, room);
        }

        internal void deleteLocation(Guid regionId, Guid suburbId, Guid venueId, Guid locationId, Action<Location> action)
        {
            fetchRegion(regionId).deleteLocation(suburbId, venueId, locationId, action);
        }

        #endregion
    }
}
