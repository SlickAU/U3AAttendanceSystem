using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    internal partial class Session : ISession
    {

        public Session(Guid courseInstanceId, Guid locationId, DateTime date)
        {
            Date = date;
            CourseInstanceId = courseInstanceId;
            LocationId = locationId;    
        }

        #region Attendance Management

        internal Attendance FetchAttendance(int memberId)
        {
            return Attendances.Where(a => a.MemberId.Equals(memberId)).FirstOrDefault();
        } 

        #endregion

        #region Session Management

        internal Session update(Guid locationId, DateTime date, int visitorCount)
        {
            Date = date;
            LocationId = locationId;
            VisitorCount = visitorCount;
            return this;
        }

        internal void delete(Action<Session> action)
        {
            //Implement check for associated attendance records. If found, session cannot be deleted.
            if (Attendances.Count > 0)
            {
                throw new Exception("This session cannot be deleted as it has associated Attendance records");
            }

            action(this);
        } 

        #endregion
    }
}
