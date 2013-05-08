using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    internal partial class Attendance : IAttendance
    {
        internal Attendance() { }

        public Attendance(Member member, string presence, Session session)
        {
            MemberId = member.MemberId;
            Presence = presence;
<<<<<<< HEAD
            SessionId = session.Id;
=======
            SessionId = Session.Id;
>>>>>>> 6d5054e480df10374ac7726ba5977f89b0cce2c9
        }

        #region Attendance Management

		internal Attendance update(Member member, string presence, Session session)
        {
            MemberId = member.MemberId;
            Presence = presence;
            SessionId = session.Id;

            return this;
        }

        internal void delete(Action<Attendance> action)
        {
            action(this);
        }

	#endregion

    }
}
