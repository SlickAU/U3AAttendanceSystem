using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public interface ISession
    {
        Guid Id { get; }
        DateTime Date { get; }
        Guid CourseInstanceId { get; }
        Guid LocationId { get; }
        int VisitorCount { get; }
    }
}
