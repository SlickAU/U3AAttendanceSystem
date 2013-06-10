using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model.Partials.CourseState
{[Serializable]
    internal class Complete : CourseInstanceState
    {
        
        internal override CourseInstance.State State
        {
            get { return CourseInstance.State.Complete; }
        }

    }
}
