using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public interface ISuburb
    {
        Guid Id { get; }
        Guid RegionId { get; }
        string Name { get; }
        int PostCode { get; }
        string Description { get; }
        bool HasInstances { get; }
    }
}
