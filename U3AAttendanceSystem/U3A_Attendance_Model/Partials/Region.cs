using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    internal partial class Region : IRegion
    {
        //Region constructor
        public Region(string codeId, string description, Guid u3aId)
        {
            CodeId = codeId;
            Description = description;
            U3AId = u3aId;
        }

        #region Region Management

        internal Region update(string codeId, string description, Guid u3aId)
        {
            CodeId = codeId;
            Description = description;
            U3AId = u3aId;

            return this;
        }

        internal void delete(Action<Region> action)
        {
            if (this.CourseInstances.Count > 0)
            {
                throw new BusinessRuleException("Region cannot be deleted as it is associated with Course Instances.");
            }
            else if(this.Suburbs.Count > 0)
            {
                throw new BusinessRuleException("Region cannot be deleted as it is associated with Suburbs.");
            }

            action(this);
        }

        #endregion

        #region Course Instance Management

        internal CourseInstance createCourseInstance(CourseDescription description, Coordinator coordinator, Guid suburbId,Guid venueId,Guid defaultLocationId, DateTime startDate, string courseCode)
        {
            var location = fetchLocation(venueId, suburbId, defaultLocationId); 
            var courseInstance = new CourseInstance(this.Id, description, coordinator, location, startDate, courseCode);
            this.CourseInstances.Add(courseInstance); 

            return courseInstance;
        }

        internal CourseInstance updateCourseInstance(Guid courseInstanceId, Coordinator coordinator, Guid suburbId, Guid venueId, Guid locationId, DateTime startDate, int stateId, string courseCode)
        {
            var location = fetchSuburb(suburbId).fetchLocation(venueId, locationId);
            var courseInstance = fetchCourseInstance(courseInstanceId);
            return courseInstance.update(this.Id, coordinator, location, startDate, stateId, courseCode);
        }

        internal CourseInstance fetchCourseInstance(Guid courseInstanceId)
        {
            var result = this.CourseInstances.Where(i => i.Id.Equals(courseInstanceId)).FirstOrDefault();

            if (result == null)
            {
                throw new BusinessRuleException("Invalid region identifier supplied");
            }
            
            return result; 
        }

        internal IEnumerable<CourseInstance> fetchCourseInstances()
        {
            var result = CourseInstances.AsEnumerable();

            if (result == null)
            {
                throw new BusinessRuleException("Could not obtain collection of Course Instances");
            }
           
            return result;
        }

        internal void deleteCourseInstance(Guid id,  Action<CourseInstance> action)
        {
            var course = fetchCourseInstance(id);
            course.delete(action); 
        }

        #endregion

        #region Session Management

        internal Session createSession(Guid courseInstanceId, Guid suburbId, Guid venueId, Guid locationId, DateTime date)
        {
            var courseInstance = fetchCourseInstance(courseInstanceId);
            var location = fetchLocation(venueId, suburbId, locationId);

            return courseInstance.createSession(location, date);
        }

        internal Session updateSession(Guid sessionId, Guid locationId, DateTime date, int visitorCount, Guid courseInstanceId)
        {
            return fetchCourseInstance(courseInstanceId).updateSession(sessionId, locationId, date, visitorCount);
        }

        internal ISession fetchSession(Guid sessionId, Guid courseInstanceId)
        {
            return fetchCourseInstance(courseInstanceId).fetchSession(sessionId);
        }

        internal IEnumerable<Session> fetchSessions(Guid courseInstanceId)
        {
            return fetchCourseInstance(courseInstanceId).fetchSessions();
        }

        internal void deleteSession(Guid sessionId, Guid courseInstanceId, Action<Session> action)
        {
            fetchCourseInstance(courseInstanceId).deleteSession(sessionId, action);
        }

        #endregion

        #region Attendance Management

        internal Attendance createAttendance(Guid courseInstanceId, Guid sessionId, Member member, string presence)
        {
            return fetchCourseInstance(courseInstanceId).createAttendance(sessionId, member, presence);
        }

        internal Attendance fetchAttendance(Guid courseInstanceId, Guid sessionId, Guid attendanceId)
        {
            return fetchCourseInstance(courseInstanceId).fetchAttendance(sessionId, attendanceId);
        }

        internal IEnumerable<Attendance> fetchAttendances(Guid courseInstanceId, Guid sessionId)
        {
            return fetchCourseInstance(courseInstanceId).fetchAttendances(sessionId);
        }

        internal Attendance updateAttendance(Guid courseInstanceId, Guid sessionId, Guid attendanceId, Member member, string presence)
        {
            return fetchCourseInstance(courseInstanceId).updateAttendance(sessionId, attendanceId, member, presence);
        }

        internal void deleteAttendance(Guid courseInstanceId, Guid sessionId, Guid attendanceId, Action<Attendance> action)
        {
            fetchCourseInstance(courseInstanceId).deleteAttendance(sessionId, attendanceId, action);
        }

        #endregion

        #region Suburb Management

        internal Suburb fetchSuburb(Guid suburbId)
        {
            var result = Suburbs.Where(s => s.Id.Equals(suburbId)).FirstOrDefault();

            if (result == null)
            {
                throw new BusinessRuleException("Invalid Suburb identifier supplied");
            }

            return result;
        }

        internal IEnumerable<Suburb> fetchSuburbs()
        {
            var result = Suburbs.AsEnumerable();

            if (result == null)
            {
                throw new BusinessRuleException("Invalid Suburb identifier supplied");
            }

            return result;
        }

        internal IEnumerable<Suburb> fetchAllSuburbs()
        {
            var result = Suburbs.AsEnumerable();

            if (result == null)
            {
                throw new BusinessRuleException("Invalid Suburb identifier supplied");
            }

            return result;
        }

        #endregion

        #region Venue Management

        internal Venue createVenue(Guid suburbId, string name, string address, string codeId)
        {
            return fetchSuburb(suburbId).createVenue(name, address, codeId);
        }

        internal Venue updateVenue(Guid venueId, Guid suburbId, string name, string address, string codeId)
        {
            return fetchSuburb(suburbId).updateVenue(venueId, name, address, codeId);
        }

        internal Venue fetchVenue(Guid venueId, Guid suburbId)
        {
            return fetchSuburb(suburbId).fetchVenue(venueId);
        }

        internal IEnumerable<Venue> fetchVenues(Guid suburbId)
        {
            return fetchSuburb(suburbId).fetchVenues();
        }

        internal IEnumerable<Venue> fetchAllVenues()
        {
            List<Venue> venueList = new List<Venue>();

            foreach (var s in Suburbs)
            {
                var value = s.fetchAllVenues();

                if (!value.Count().Equals(0))
                {
                    foreach(var v in value.ToList())
                    {
                        venueList.Add(v);
                    }
                }
            }

            return venueList.AsEnumerable();
        }

        internal void deleteVenue(Guid venueId, Guid suburbId, Action<Venue> action)
        {
            fetchSuburb(suburbId).deleteVenue(venueId, action);
        }

        #endregion

        #region Location Management

        internal Location createLocation(Guid suburbId, Guid venueId, string room)
        {
            return fetchSuburb(suburbId).createLocation(venueId, room);
        }
               
        internal Location fetchLocation(Guid venueId, Guid suburbId, Guid locationId)
        {
            return fetchVenue(venueId, suburbId).fetchLocation(locationId);
        }

        internal IEnumerable<Location> fetchLocations(Guid suburbId, Guid venueId)
        {
            return fetchSuburb(suburbId).fetchLocations(venueId);
        }

        internal Location updateLocation(Guid suburbId, Guid venueId, Guid locationId, string room)
        {
            return fetchSuburb(suburbId).updateLocation(venueId, locationId, room);
        }

        internal void deleteLocation(Guid suburbId, Guid venueId, Guid locationId, Action<Location> action)
        {
            fetchSuburb(suburbId).deleteLocation(venueId, locationId, action);
        }

        #endregion

    }
}