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
            var result = CourseDescriptions.Where(c => c.Id.Equals(courseDescriptionId)).FirstOrDefault();
            
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

        //Fetches CourseDescriptionInstances
        internal IEnumerable<CourseInstance> fetchCourseDescriptionInstances(Guid courseDescriptionId)
        {
            return fetchCourseDescription(courseDescriptionId).fetchCourseInstances();
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

        internal IEnumerable<CourseInstance> fetchCourseInstancesByDescription(Guid courseDescriptionId)
        {
            return fetchCourseDescription(courseDescriptionId).fetchCourseInstances();
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

        #region Attendance Management

        internal Attendance createAttendance(Guid regionId, Guid courseInstanceId, Guid sessionId, int memberId, string presence)
        {
            var member = fetchMember(memberId);

            return fetchRegion(regionId).createAttendance(courseInstanceId, sessionId, member, presence);
        }

        internal Attendance fetchAttendance(Guid regionId, Guid courseInstanceId, Guid sessionId, Guid attendanceId)
        {
            return fetchRegion(regionId).fetchAttendance(courseInstanceId, sessionId, attendanceId);
        }

        internal IEnumerable<Attendance> fetchAttendances(Guid regionId, Guid courseInstanceId, Guid sessionId)
        {
            return fetchRegion(regionId).fetchAttendances(courseInstanceId, sessionId);
        }

        internal IEnumerable<Member> fetchAttendances(Guid regionId, Guid courseInstanceId)
        {
            return fetchRegion(regionId).fetchAttendances(courseInstanceId);
        }

        internal Attendance updateAttendance(Guid regionId, Guid courseInstanceId, Guid sessionId, Guid attendanceId, int memberId, string presence)
        {
            var member = fetchMember(memberId);

            return fetchRegion(regionId).updateAttendance(courseInstanceId, sessionId, attendanceId, member, presence);
        }

        internal void deleteAttendance(Guid regionId, Guid courseInstanceId, Guid sessionId, Guid attendanceId, Action<Attendance> action)
        {
            fetchRegion(regionId).deleteAttendance(courseInstanceId, sessionId, attendanceId, action);
        }

        #endregion

        #region Region Management

        internal Region createRegion(string codeId, string description)
        {
            var region = new Region(codeId, description, this.Id);
            this.Regions.Add(region);
            return region;
        }

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
            var result = Regions.AsEnumerable();

            if (result == null)
            {
                throw new BusinessRuleException("No regions exist.");
            }

            return result; 
        }

        internal Region updateRegion(Guid regionId, string codeId, string description)
        {
            return fetchRegion(regionId).update(codeId, description, this.Id);
        }

        internal void deleteRegion(Guid regionId, Action<Region> action)
        {
            fetchRegion(regionId).delete(action);
        }
        #endregion

        #region Suburb Management

        internal IEnumerable<Suburb> fetchSuburbs(Guid regionId)
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

        internal IEnumerable<Venue> fetchAllVenues()
        {
            List<Venue> venueList = new List<Venue>();

            foreach (var r in Regions)
            {
                var value = r.fetchAllVenues() ;

                if (value != null)
                {
                    foreach (var v in value)
                    {
                        venueList.Add(v);
                    }
                }
            }

            return venueList.AsEnumerable();

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

        #region Member Management

        //Creates Member
        internal Member createMember(int memberId)
        {
            var member = new Member(memberId, this.Id);
            this.Members.Add(member);
            return member;
        }

        //Updates Member
        internal Member updateMember(int memberId)
        {
           return fetchMember(memberId).update(this.Id);
        }

        //Fetches single Member
        internal Member fetchMember(int memberId)
        {
            var result = Members.Where(m => m.MemberId.Equals(memberId)).FirstOrDefault();

            if (result == null)
            {
                throw new BusinessRuleException("Invalid Member Identifier supplied");
            }

            return result;
        }

        //Fetches collection of members
        internal IEnumerable<Member> fetchMembers()
        {
            var result = Members.AsEnumerable();

            if (result == null)
            {
                throw new BusinessRuleException("Could not obtain collection of Members.");
            }

            return result;
        }

        //Deletes a Member
        internal void deleteMember(int memberId, Action<Member> action)
        {
            fetchMember(memberId).delete(action);
        }

        #endregion
    }
}
