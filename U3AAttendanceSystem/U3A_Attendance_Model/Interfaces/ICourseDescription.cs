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
<<<<<<< HEAD
=======
         bool HasInstances { get; }
>>>>>>> 6d5054e480df10374ac7726ba5977f89b0cce2c9
    }
}
