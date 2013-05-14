using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public interface ICoordinator
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
        string Phone { get; }
    }
}
