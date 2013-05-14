using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace U3A_Attendance_Model
{
    public interface IMember
    {
        Guid U3AId { get; }
        int MemberId { get; }
    }
}
