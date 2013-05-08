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
<<<<<<< HEAD
<<<<<<< HEAD
=======
        bool HasVenues { get; }
>>>>>>> 6d5054e480df10374ac7726ba5977f89b0cce2c9
=======
        bool HasVenues { get; }
>>>>>>> 6d5054e480df10374ac7726ba5977f89b0cce2c9
    }
}
