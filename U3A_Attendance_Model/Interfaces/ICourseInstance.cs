using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public interface ICourseInstance
    {
        Guid Id { get; }
        DateTime StartDate { get; }
        Guid CourseId { get; }
        Guid RegionId { get; }
        Guid CoordinatorId { get; }
        Guid DefaultLocationId { get; }
        string CourseCode { get; }
        int StateId { get; }
        Guid SuburbId { get; }
        Guid VenueId { get; }
        IEnumerable<ISession> Sessions { get; }
        ICourseDescription CourseDescription { get; }
    }
}
