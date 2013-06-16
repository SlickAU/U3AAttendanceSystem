using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model.Partials.CourseState
{[Serializable]
    internal class Cancelled : CourseInstanceState
    {
        
        internal override void delete(Action action)
        {
            action(); 
        }


        internal override CourseInstance.State State
        {
            get { return CourseInstance.State.Cancelled; }
        }
    }
}
