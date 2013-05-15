using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public interface IVenue
    {
        Guid Id { get; }
        string Name { get; }
        string Address { get; }
        string CodeId { get; }
        Guid SuburbId { get; }
        IEnumerable<ILocation> Locations {get;}
    }
}
