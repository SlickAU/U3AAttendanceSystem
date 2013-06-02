using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    internal abstract class CourseInstanceState
    {


        internal abstract CourseInstance.State State {get;}


        internal virtual void delete(Action action)
        {
            //NO-OP by default
        }
    }
}
