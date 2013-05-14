using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
     public class CourseDescriptionInstancesViewModel : BaseViewModel
    {
        // string CourseCode { get; set; }
         public IEnumerable<ICourseInstance> CourseInstances { get; set; }

         public CourseDescriptionInstancesViewModel(ICourseDescription cd)
         {
             //CourseCode = cd.CourseNumber.ToString();
             CourseInstances = _facade.FetchCourseInstancesByDescription(cd.Id);
         }
    }
}
