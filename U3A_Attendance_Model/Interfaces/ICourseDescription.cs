using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public interface ICourseDescription
    {
        Guid Id { get; }
        int CourseNumber { get; }
        string Title { get; }
        string Description { get; }
        Guid U3AId { get; }
        bool HasInstances { get; }
        IEnumerable<ICourseInstance> CourseInstances { get; }
    }
}
