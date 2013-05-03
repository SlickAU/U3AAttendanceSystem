using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public class Facade : IDisposable
    {
        private U3AAttendanceEntities _context = new U3AAttendanceEntities();
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

        #region U3A Management

        private U3A fetchU3a()
        {
            var u3a = _context.U3A
                .Include("Regions")
                .Include("CourseDescriptions")
                .Include("CourseDescriptions.CourseInstances")
                .Include("CourseDescriptions.CourseInstances.Sessions")
                .Include("Coordinators")
                .Include("Regions.Suburbs.Venues")
                .Include("Regions.Suburbs.Venues.Locations")
                .Include("Regions.Suburbs").FirstOrDefault();

            if (u3a == null)
            {
                throw new BusinessRuleException("Unable to locate U3A");
            }

            return u3a;
        }

        //Used to instantiate different U3A instances
        private U3A fetchU3a(Guid u3aId)
        {
            var u3a = _context.U3A
                .Include("Regions")
                .Include("Regions.Suburbs")
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

        public IEnumerable<ICourseInstance> FetchCourseInstancesByDescription(Guid descriptionId)
        {
            return _u3a.fetchCourseInstancesByDescription(descriptionId);
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


        public string GenerateCourseCode(DateTime startDate, Guid venueId, Guid regionId, Guid suburbId)
        {
            var venue = FetchVenue(venueId, regionId, suburbId);
            var region = retrieveRegion(regionId);
            string courseCode;
            string semester;

            //Implement increment checker to see if course code number already exists - Current value is hardcoded
            //Exception handling on UI to make sure Date, Venue and Region fields have been filled

            string tempYear = startDate.Year.ToString();
            string year = tempYear.Substring(2);
            int month = startDate.Month;
            string regionL = region.CodeId.ToString();
            string venueL = venue.CodeId.ToString();

            if (month <= 6)
            {
                semester = "1";
            }

            else
            {
                semester = "2";
            }

            courseCode = string.Format("{0}{1}{2}{3}", year, semester, regionL, venueL);

            //foreach (CourseInstance c in CourseInstances)
            //{
            //    if (c.CourseCode == courseCode)
            //    {
            //        //DO WORK HERE
            //    }
            //}

            return courseCode;
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

        //Updates a session
        public ISession UpdateSession(Guid sessionId, Guid locationId, DateTime date, int visitorCount, Guid courseInstanceId, Guid regionId)
        {
            return _u3a.updateSession(sessionId, locationId, date, visitorCount, courseInstanceId, regionId);
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

        #endregion

        #region Suburb Management

        //Fetches all suburbs for a region
        public IEnumerable<ISuburb> FetchSuburbs(Guid regionId)
        {
            return _u3a.fetchSuburbs(regionId);
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

        //Fetches multiple Venues
        public IEnumerable<IVenue> FetchVenues(Guid regionId, Guid suburbId)
        {
            return _u3a.fetchVenues(regionId, suburbId);
        }

        //Updates a Venue
        public IVenue UpdateVenue(Guid venueId, Guid regionId, Guid suburbId, string name, string address, string codeId)
        {
            var venue = _u3a.updateVenue(venueId, regionId, suburbId, name, address, codeId);            
            _context.SaveChanges();
            return venue;
        }

        public IEnumerable<IVenue> AllVenues()
        {
            return _u3a.allVenues();
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

        //int memberNumber, string presence, Guid sessionId

        public IAttendance AddAttendance()
        {
            //var member = retrieveMember(at.memberNumber);
            //var a = new Attendance(member.Id, presence, sessionId);
            return null; //TODO:
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
        /// <summary>
        /// Fetch U3A Regions
        /// NOTE: Added by Kevin
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IRegion> FetchRegions()
        {
            return _u3a.fetchRegions();
        }

        internal Region retrieveRegion(Guid Id)
        {
            return _context.Regions.FirstOrDefault(region => region.Id.Equals(Id));
        }
        #endregion

        #region MemberManagement

        private Member retrieveMember(int memberNumber)
        {
            var memberExists = _context.Members.FirstOrDefault(m => m.MemberId.Equals(memberNumber));

            if (memberExists == null)
            {
                createMember(memberNumber);
            }

            return _context.Members.FirstOrDefault(member => member.MemberId.Equals(memberNumber));
        }

        /// <summary>
        /// Testing for duplicates etc...
        /// </summary>
        /// <param name="memberNumber"></param>
        public void CreateMember(int memberNumber)
        {
            CreateMember(memberNumber);
        }

        private Member createMember(int memberNumber)
        {
            var member = new Member();
            _context.Members.Add(member);
            _context.SaveChanges();

            return member;
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

