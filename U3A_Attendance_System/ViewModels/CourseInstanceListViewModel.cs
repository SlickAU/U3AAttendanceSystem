using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class CourseInstanceListViewModel : BaseViewModel
    {
        public IEnumerable<ICourseInstance> InstancesList
        { get { return _facade.AllInstances(); } }
    }
}
