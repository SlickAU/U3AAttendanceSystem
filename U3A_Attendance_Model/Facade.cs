using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model.Interfaces;

namespace U3A_Attendance_Model
{
    public class Facade : IDisposable
    {
        private U3AAttendanceEntities _context = new U3AAttendanceEntities();
        private U3A.SearchEngine _searchEngine = new U3A.SearchEngine(); 
        private U3A _u3a;

        //Facade Constructor
        public Facade()
        {
            _u3a = fetchU3a();
        }

        public Facade(Guid u3aId)
        {
            _u3a = fetchU3a(u3aId);
            

        }


        public void Examples()
        {
            //TODO: Implement somewhere (Example Only)
            var result = _searchEngine.Search((FetchCourseDescriptions() as IEnumerable<ISearchable>), "a"); 
     
        }

        #region U3A Management

        //Retrives first instance of U3A in database
        private U3A fetchU3a()
        {
            var u3a = _context.U3A
                .Include("Regions.Suburbs.Venues.Locations")
                .Include("CourseDescriptions.CourseInstances.Sessions.Attendances")
                .Include("Coordinators")
                .Include("Members.Attendances")
                .FirstOrDefault();
            
            if (u3a == null)
            {
                throw new BusinessRuleException("Could not locate U3A instance");
            }

            return u3a;
        }

        //Used to instantiate different U3A instances
        private U3A fetchU3a(Guid u3aId)
        {
            var u3a = _context.U3A
                .Include("Regions.Suburbs.Venues.Locations")
                .Include("CourseDescriptions.CourseInstances.Sessions")
                .Include("Coordinators")
                .Include("Members.Attendances")
                .Where(u => u.Id.Equals(u3aId)).First();
            
            if (u3a == null)
            {
                throw new BusinessRuleException("Invalid U3A identifier supplied");
            }

            return u3a;
        }

        #endregion

        #region Course Description Management

        //Creates a CourseDescription
        public ICourseDescription CreateCourseDescription(string title, string description)
        {
            var courseDescription = _u3a.createCourseDescription(title, description);
            _context.SaveChanges();
            return courseDescription;
        }

        //Updates a CourseDescription
        public ICourseDescription UpdateCourseDescription(Guid courseDescriptionId, string title, string description)
        {
            var courseDescription = _u3a.updateCourseDescription(courseDescriptionId, title, description);
            _context.SaveChanges();
            return courseDescription;
        }

        //Fetches a CourseDescription
        public ICourseDescription FetchCourseDescription(Guid courseDescriptionId)
        {
            return _u3a.fetchCourseDescription(courseDescriptionId);
        }

        //Fetches multiple CourseDescriptions
        public IEnumerable<ICourseDescription> FetchCourseDescriptions()
        {
            return _u3a.fetchCourseDescriptions();
        }

        //Fetches CourseDescriptionInstances
        public IEnumerable<ICourseInstance> FetchCourseDescriptionInstances(Guid courseDescriptionId)
        {
            return _u3a.fetchCourseDescriptionInstances(courseDescriptionId);
        }

        //Deletes CourseDescription
        public void DeleteCourseDescription(Guid courseDescriptionId)
        {
            Action<CourseDescription> action
                = delegate(CourseDescription c)
                {
                    _context.CourseDescriptions.Remove(c);
                    _context.SaveChanges();
                };

            _u3a.deleteCourseDescription(courseDescriptionId, action);
        }

        #endregion

        #region Course Instance Management

        //Creates a CourseInstance
        public ICourseInstance CreateCourseInstance(Guid courseDescriptionId, Guid coordinatorId, Guid regionId,Guid suburbId, Guid venueId, Guid defaultLocationId, DateTime startDate, string courseCode)
        {
            var courseInstance = _u3a.createCourseInstance(courseDescriptionId, regionId, coordinatorId, suburbId, venueId, defaultLocationId, startDate, courseCode);
            _context.SaveChanges();
            return courseInstance;
        }

        //Fetches a Course Instance
        public ICourseInstance FetchCourseInstance(Guid courseInstanceId, Guid regionId)
        {
            return _u3a.fetchCourseInstance(courseInstanceId, regionId); 
        }

        //Fetches multiple Course Instances
        public IEnumerable<ICourseInstance> FetchCourseInstances(Guid regionId)
        {
            return _u3a.fetchCourseInstances(regionId);
        }

        //Fetches CourseInstances that share the same CourseDescription Template
        public IEnumerable<ICourseInstance> FetchCourseInstancesByDescription(Guid courseDescriptionId)
        {
            return _u3a.fetchCourseInstancesByDescription(courseDescriptionId);
        }

        //Updates a CourseInstance
        public ICourseInstance UpdateCourseInstance(Guid courseInstanceId, Guid coordinatorId, Guid regionId, Guid suburbId, Guid venueId, Guid defaultLocationId, DateTime startDate, int stateId, string courseCode)
        {
            var courseInstance = _u3a.updateCourseInstance(courseInstanceId, coordinatorId, regionId, suburbId, venueId, defaultLocationId, startDate, stateId, courseCode);        
            _context.SaveChanges();
            return courseInstance;
        }

        //Deletes a CourseInstance
        public void DeleteCourseInstance(Guid courseInstanceId, Guid regionId)
        {
            Action<CourseInstance> action 
                = delegate(CourseInstance i) 
                { 
                    _context.CourseInstances.Remove(i); 
                    _context.SaveChanges();
                }; 
           
            _u3a.deleteCourseInstance(courseInstanceId, regionId, action); 
        }

        public string CheckCourseCode(string courseCode, Guid regionId, Guid venueId)
        {
            //Implement increment checker to see if course code number already exists - Current value is hardcoded
            //Exception handling on UI to make sure Date, Venue and Region fields have been filled

            var courses = FetchCourseInstances(regionId);
            List<string> coursecodes = new List<string>();
            List<string> courseNums = new List<string>();
            string cnum;
            int num;

            foreach (var c in courses.Where(v => v.VenueId.Equals(venueId)))
            {
                string cc = c.CourseCode;
                coursecodes.Add(cc);
            }

            foreach (var c in coursecodes)
            {
                var courseNum = c.Substring(5, 2);
                courseNums.Add(courseNum);
            }

            if (courseNums.Count != 0)
            {
                var ordered = courseNums.OrderByDescending(c => c.FirstOrDefault());

                var value = ordered.First();
                num = Int32.Parse(value);

                num++;

                cnum = num.ToString();

                if (cnum.Length == 1)
                {
                    string result = "0" + cnum;
                    return courseCode + result;
                }

                else
                {
                    return courseCode + cnum;
                }
            }

            else return courseCode + "01";
        }
        

        #endregion

        #region Course Session Management

        //Creates a session
        public ISession CreateSession(Guid courseInstanceId, Guid regionId, Guid suburbId, Guid venueId, Guid locationId, DateTime date)
        {
            var session = _u3a.createSession(courseInstanceId, regionId, suburbId, venueId, locationId, date);
            _context.SaveChanges();
            return session;
        }

        public IEnumerable<ICourseInstance> AllInstances()
        {
            return _context.CourseInstances.ToList();
        }

        //Fetches a single session
        public ISession FetchSession(Guid sessionId, Guid courseInstanceId, Guid regionId)
        {
            return _u3a.fetchSession(sessionId, courseInstanceId, regionId);
        }

        //Fetches multiple sessions
        public IEnumerable<ISession> FetchSessions(Guid courseInstanceId, Guid regionId)
        {
            return _u3a.fetchSessions(courseInstanceId, regionId);
        }

        //Updates a session
        public ISession UpdateSession(Guid sessionId, Guid locationId, DateTime date, int visitorCount, Guid courseInstanceId, Guid regionId)
        {
            var session = _u3a.updateSession(sessionId, locationId, date, visitorCount, courseInstanceId, regionId);
            _context.SaveChanges();
            return session;
        }

        //Deletes a Session 
        public void DeleteSession(Guid sessionId, Guid courseInstanceId, Guid regionId)
        {
            Action<Session> action
                = delegate(Session s)
                {
                    _context.Sessions.Remove(s);
                    _context.SaveChanges();
                }; 
            
            _u3a.deleteSession(sessionId, courseInstanceId, regionId, action);
        }

        #endregion++++-

        #region Suburb Management

        public IEnumerable<ISuburb> FetchSuburbs(Guid regionId)
        {
            return _u3a.fetchSuburbs(regionId);
        }

        public IEnumerable<ISuburb> FetchSuburbsWithVenues(Guid regionId)
        {
            return _u3a.fetchSuburbs(regionId).Where(v => v.HasVenues);
        }

        #endregion

        #region Venue Management

        //Creates a Venue
        public IVenue CreateVenue(Guid regionid, Guid suburbId, string name, string address, string codeId)
        {
            var venue = _u3a.createVenue(regionid, suburbId, name, address, codeId);
            _context.SaveChanges();
            return venue;
        }

        //Fetches a Venue
        public IVenue FetchVenue(Guid venueId, Guid regionId, Guid suburbId)
        {
            return _u3a.fetchVenue(venueId, regionId, suburbId);
        }

        //Fetches multiple Venues related to a suburb
        public IEnumerable<IVenue> FetchVenues(Guid regionId, Guid suburbId)
        {
            return _u3a.fetchVenues(regionId, suburbId);
        }

        //Fetches all venues in the database
        public IEnumerable<IVenue> FetchAllVenues()
        {
            return _u3a.fetchAllVenues();
        }

        //Updates a Venue
        public IVenue UpdateVenue(Guid venueId, Guid regionId, Guid suburbId, string name, string address, string codeId)
        {
            var venue = _u3a.updateVenue(venueId, regionId, suburbId, name, address, codeId);            
            _context.SaveChanges();
            return venue;
        }

        //Deletes a Venue
        public void DeleteVenue(Guid venueId, Guid regionId, Guid suburbId)
        {
            Action<Venue> action
                = delegate(Venue v)
                {
                    _context.Venues.Remove(v);
                    _context.SaveChanges();
                };

            _u3a.deleteVenue(venueId, regionId, suburbId, action);
        }

        #endregion

        #region Location Management

        //Creates a Location
        public ILocation CreateLocation(Guid regionId, Guid suburbId, Guid venueId, string room)
        {
            var location = _u3a.createLocation(regionId, suburbId, venueId, room);
            _context.SaveChanges();
            return location;
        }

        //Fetches a Location
        public ILocation FetchLocation(Guid regionId, Guid suburbId, Guid venueId, Guid locationId)
        {
            return _u3a.fetchLocation(regionId, suburbId, venueId, locationId);
        }
        
        //Fetches multiple Locations
        public IEnumerable<ILocation> FetchLocations(Guid regionId, Guid suburbId, Guid venueId)
        {
            return _u3a.fetchLocations(regionId, suburbId, venueId);
        }

        //Updates a Location
        public ILocation UpdateLocation(Guid regionId, Guid suburbId, Guid venueId, Guid locationId, string room)
        {
            var location = _u3a.updateLocation(regionId, suburbId, venueId, locationId, room);
            _context.SaveChanges();
            return location;
        }

        //Deletes a Location
        public void DeleteLocation(Guid regionId, Guid suburbId, Guid venueId, Guid locationId)
        {
            Action<Location> action
                = delegate(Location l)
                {
                    _context.Locations.Remove(l);
                    _context.SaveChanges();
                };

            _u3a.deleteLocation(regionId, suburbId, venueId, locationId, action);
        }

        #endregion

        #region Attendance Management

        public IAttendance CreateAttendance(Guid regionId, Guid courseInstanceId, Guid sessionId, int memberId, string presence)
        {
            var attendance = _u3a.createAttendance(regionId, courseInstanceId, sessionId, memberId, presence);
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
            }
            return attendance;
        }

        public IAttendance FetchAttendance(Guid regionId, Guid courseInstanceId, Guid sessionId, Guid attendanceId)
        {
            return _u3a.fetchAttendance(regionId, courseInstanceId, sessionId, attendanceId);
        }

        public IEnumerable<IAttendance> FetchAttendances(Guid regionId, Guid courseInstanceId, Guid sessionId)
        {
            return _u3a.fetchAttendances(regionId, courseInstanceId, sessionId);
        }

        public IEnumerable<IMember> FetchAttendances(Guid regionId, Guid courseInstanceId)
        {
            return _u3a.fetchAttendances(regionId, courseInstanceId);
        }

        public IAttendance UpdateAttendance(Guid regionId, Guid courseInstanceId, Guid sessionId, Guid attendanceId, int memberId, string presence)
        {
            var attendance = _u3a.updateAttendance(regionId, courseInstanceId, sessionId, attendanceId, memberId, presence);
            _context.SaveChanges();
            return attendance;
        }

        public void DeleteAttendance(Guid regionId, Guid courseInstanceId, Guid sessionId, Guid attendanceId)
        {
            Action<Attendance> action = 
                delegate(Attendance a)
                {
                    _context.Attendances.Remove(a);
                    _context.SaveChanges();
                };

            _u3a.deleteAttendance(regionId, courseInstanceId, sessionId, attendanceId, action);
        }
        
        #endregion

        #region Coordinator Management

        //Creates a Coordinator
        public ICoordinator CreateCoordinator(string name, string email, string phoneNumber)
        {
            var coordinator = _u3a.createCoordinator(name, email, phoneNumber);
            _context.SaveChanges();
            return coordinator;
        }
        
        //Fetches a Coordinator
        public ICoordinator FetchCoordinator(Guid coordinatorId)
        {
            return _u3a.fetchCoordinator(coordinatorId);
        }

        //Fetches multiple Coordinators
        public IEnumerable<ICoordinator> FetchCoordinators()
        {
            return _u3a.Coordinators;
        }

        //Updates a Coordinator
        public ICoordinator UpdateCoordinator(Guid coordinatorId, string name, string email, string phoneNumber)
        {
            var coordinator = _u3a.updateCoordinator(coordinatorId, name, email, phoneNumber);
            _context.SaveChanges();
            return coordinator;
        }

        //Deletes a Coordinator
        public void DeleteCoordinator(Guid coordinatorId)
        {
            Action<Coordinator> action
                = delegate(Coordinator c)
                {
                    _context.Coordinators.Remove(c);
                    _context.SaveChanges();
                };

            _u3a.deleteCoordinator(coordinatorId, action);
        }

        #endregion

        #region Region Management

        public IRegion CreateRegion(string codeId, string description)
        {
            var region = _u3a.createRegion(codeId, description);
            _context.SaveChanges();
            return region;
        }

        public IRegion FetchRegion(Guid regionId) 
        {
            return _u3a.fetchRegion(regionId);
        }

        public IEnumerable<IRegion> FetchRegions()
        {
            return _u3a.fetchRegions();
        }

        public IRegion UpdateRegion(Guid regionId, string codeId, string description)
        {
            var region = _u3a.updateRegion(regionId, codeId, description);
            _context.SaveChanges();
            return region;
        }

        public void DeleteRegion(Guid regionId)
        {
            Action<Region> action = 
                delegate(Region r)
                {
                    _context.Regions.Remove(r);
                    _context.SaveChanges();
                };

            _u3a.deleteRegion(regionId, action);
        }

        #endregion

        #region Member Management

        //Creates a Member
        public IMember CreateMember(int memberId)
        {
            var member = _u3a.createMember(memberId);
            _context.SaveChanges();
            return member;
        }

        //Fetches single Member
        public IMember FetchMember(int memberId)
        {
            return _u3a.fetchMember(memberId);
        }

        //Fetches collection of Members
        public IEnumerable<IMember> FetchMembers()
        {
            return _u3a.fetchMembers();
        }

        //Updates a Member
        public IMember UpdateMember(int memberId)
        {
            var member = _u3a.updateMember(memberId);
            _context.SaveChanges();
            return member;
        }

        //Deletes a Member
        public void DeleteMember(int memberId)
        {
            Action<Member> action =  
                delegate(Member m)
                {
                    _context.Members.Remove(m);
                    _context.SaveChanges();
                };

            _u3a.deleteMember(memberId, action);
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

