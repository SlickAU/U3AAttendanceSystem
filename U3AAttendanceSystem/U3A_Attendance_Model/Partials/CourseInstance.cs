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

            else 
            {
                StateId = 1;
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

        internal Attendance fetchAttendance(int memberId, Guid sessionId)
        {
            var session = Sessions.Where(s => s.Id.Equals(sessionId)).FirstOrDefault();
            return session.FetchAttendance(memberId);
        }

        #endregion

        #region Session Management

        internal Session createSession(Location location, DateTime date)
        {
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


        public Guid SuburbId
        {
            get { return Location.Venue.SuburbId; }
        }


        public Guid VenueId
        {
            get { return Location.VenueId; }
        }


        public IEnumerable<ISession> CourseSessions
        {
            get { return Sessions; }
        }
    }
}
