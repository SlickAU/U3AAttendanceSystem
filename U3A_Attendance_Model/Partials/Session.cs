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
            Attendances = new List<Attendance>();
        }

        #region Attendance Management

        internal Attendance createAttendance(Member member, string presence)
        {
            //Check for existing attendance record,
            //If exists, update
            var attendance = this.Attendances.Where(m => m.MemberId.Equals(member.MemberId)).FirstOrDefault();

            if (attendance != null)
            {
                attendance.update(member, presence, this);
            }
            else
            {
                attendance = new Attendance(member, presence, this);
                this.Attendances.Add(attendance);
            }
            return attendance;
        }

        internal Attendance fetchAttendance(Guid attendanceId)
        {
            var attendance = Attendances.Where(a => a.Id.Equals(attendanceId)).FirstOrDefault();

            if(attendance == null)
            {
                throw new BusinessRuleException("Invalid attendance identifier supplied");
            }
            return attendance;
        }

        internal IEnumerable<Attendance> fetchAttendances()
        {
            var attendance = Attendances.AsEnumerable();

            if (attendance == null)
            {
                throw new BusinessRuleException("Invalid attendance identifier supplied");
            }

            return attendance;
        }

        internal Attendance updateAttendance(Guid attendanceId, Member member, string presence)
        {
            return fetchAttendance(attendanceId).update(member, presence, this);
        }

        internal void deleteAttendance(Guid attendanceId, Action<Attendance> action)
        {
            fetchAttendance(attendanceId).delete(action);
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
            if (Attendances.Count > 0)
            {
                throw new Exception("This session cannot be deleted as it has associated Attendance records");
            }

            action(this);
        } 

        #endregion


        IEnumerable<IAttendance> ISession.Attendances
        {
            get { return Attendances; }
        }
    }
}
