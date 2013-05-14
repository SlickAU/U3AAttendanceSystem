using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace U3A_Attendance_Model
{
    public interface IU3A
    {
        Guid Id { get; }
        string Description { get; }
        IEnumerable<IMember> Members { get; }
        IEnumerable<ICourseDescription> CourseDescriptions { get; }
        IEnumerable<IRegion> Regions { get; }
    }
}
