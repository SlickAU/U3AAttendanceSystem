using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model.Interfaces
{
    public interface ISearchable
    {

        bool MeetsCritera(string keyword); 
    }
}
