using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model.Partials.CourseState; 

namespace U3A_Attendance_Model
{
    internal partial class CourseInstance : ICourseInstance
    {
        private CourseInstanceState _state;

        public Guid SuburbId
        {
            get 
            { 
                return Location.Venue.SuburbId; 
            }
        }

        public Guid VenueId
        {
            get
            {
                return Location.VenueId;
            }
        }

        public CourseInstance(Guid regionId, CourseDescription description, Coordinator coordinator, Location location, DateTime startDate, string courseCode)
        {
            StartDate = startDate;
            CourseId = description.Id; 
            RegionId = regionId;
            CoordinatorId = coordinator.Id;
            DefaultLocationId = location.Id;
            CourseCode = courseCode;
            
            if(startDate > DateTime.Today)
            {
                StateId = 2;
            }
        }

        #region Course Instance Management

        internal CourseInstance update(Guid regionId, Coordinator coordinator, Location location, DateTime startDate, int stateId, string courseCode)
        {
            RegionId = regionId;
            CoordinatorId = coordinator.Id;
            DefaultLocationId = location.Id;
            StartDate = startDate;
            StateId = stateId;  
            CourseCode = courseCode;
            return this;
        }

        internal void delete(Action<CourseInstance> action)
        {
            _state.delete(delegate() { action(this); });
        }

        #endregion

        #region Attendance Management

        internal Attendance createAttendance(Guid sessionId, Member member, string presence)
        {
            return fetchSession(sessionId).createAttendance(member, presence);
        }

        internal Attendance fetchAttendance(Guid sessionId, Guid attendanceId)
        {
            return fetchSession(sessionId).fetchAttendance(attendanceId);
        }

        internal IEnumerable<Attendance> fetchAttendances(Guid sessionId)
        {
            return fetchSession(sessionId).fetchAttendances();
        }

        internal Attendance updateAttendance(Guid sessionId, Guid attendanceId, Member member, string presence)
        {
            return fetchSession(sessionId).updateAttendance(attendanceId, member, presence);
        }

        internal void deleteAttendance(Guid sessionId, Guid attendanceId, Action<Attendance> action)
        {
            fetchSession(sessionId).deleteAttendance(attendanceId, action);
        }

        #endregion

        #region Session Management

        internal Session createSession(Location location, DateTime date)
        {
            //Check for overlapping sessions(needs to be refined for session time check)
            foreach (var s in fetchSessions())
            {
                if (s.Date.Equals(date))
                {
                    throw new BusinessRuleException("Cannot create session as one already exists");
                }
            }

            var session = new Session(this.Id, location.Id, date);
            this.Sessions.Add(session);

            return session;
        }

        internal Session fetchSession(Guid sessionId)
        {
            var result = Sessions.Where(s => s.Id.Equals(sessionId)).FirstOrDefault();

            if(result == null)
            {
                throw new BusinessRuleException("Could not obtain Course Instance Session");
            }

            return result;
        }

        internal IEnumerable<Session> fetchSessions()
        {
            var result =  Sessions.AsEnumerable();

            if (result == null)
            {
                throw new BusinessRuleException("Could not obtain collection of Course Instance Sessions");
            }

            return result;
        }

        internal Session updateSession(Guid sessionId, Guid locationId, DateTime date, int visitorCount)
        {
            return fetchSession(sessionId).update(locationId, date, visitorCount);
        }

        internal void deleteSession(Guid sessionId, Action<Session> action)
        {
            fetchSession(sessionId).delete(action);
        }

        #endregion

        #region State Management

        partial void ObjectPropertyChanged(string propertyName)
        {
            switch (propertyName)
            {
                case "StateId":
                    stateCheck();
                    break;
            }
        }

        private void stateCheck()
        {
            switch (StateId)
            {
                case 1:
                    _state = new Scheduled();
                    break;
                case 2:
                    _state = new Active();
                    break;
                case 3:
                    _state = new Complete();
                    break;
                case 4:
                    _state = new Cancelled();
                    break;
            }
        }

        #endregion

<<<<<<< HEAD


        public IEnumerable<ISession> CourseSessions
        {
            get { return Sessions.OrderBy(sesh => sesh.Date); }
        }
=======
>>>>>>> 6d5054e480df10374ac7726ba5977f89b0cce2c9
    }
}
