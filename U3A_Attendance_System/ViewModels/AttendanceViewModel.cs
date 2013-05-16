using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class AttendanceViewModel
    {


        private string[] _members;
        private Dictionary<Guid, string> _sessions = new Dictionary<Guid, string>();

        private string[,] _attendance;
        
       

        public AttendanceViewModel(ICourseInstance course)
        {
            var sessions = course.Sessions;
            course.Sessions.SelectMany(s => s.Attendances);
            var attendances = sessions.SelectMany(s => s.Attendances).ToList();
            var members = attendances.Select(a => a.Member).Distinct().ToList();
            int width = sessions.Count();
            int height = members.Count();


            constructMembersArray(members);
            constructSessionsArray(sessions); 

        }


        private void constructMembersArray(IEnumerable<IMember> members)
        {
            _members = new string[members.Count()];

            for (int i = 0; i < _members.Length; i++)
            {
                _members[i] = members.ElementAt(i).MemberId.ToString(); 
            }
        }

        private void constructSessionsArray(IEnumerable<ISession> sessions)
        {
            for (int i = 0; i < sessions.Count(); i++)
            {
                _sessions[sessions.ElementAt(i).Id] = sessions.ElementAt(i).Date.ToShortDateString();
            }
        }


        private void constructAttendanceArray(IEnumerable<IAttendance> attendances)
        {
            _attendance = new string[_sessions.Count(), _members.Count()];

            for(int i = 0; i < _sessions.Count(); i++)
            {
                for (int j = 0; j < _members.Length; j++)
                {
                    _attendance[i, j] = "p";
                }
            }

        }

    }
}
