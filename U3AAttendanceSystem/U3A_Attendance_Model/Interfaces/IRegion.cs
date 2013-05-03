using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace U3A_Attendance_Model
{
    public interface IRegion
    {
        Guid Id { get; }
        String Description { get; }
    }
}
