using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public interface IAttendance
    {
        Guid Id { get; }
        int MemberId { get; }
        string Presence { get; }
        Guid SessionId { get; }
    }
}
