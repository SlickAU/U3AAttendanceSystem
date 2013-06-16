using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using U3A_Attendance_Model;
using U3A_Attendance_System;

namespace WcfService
{
    public class Service : IService
    {
        Facade _facade = FacadeFactory.Instance();
        ICourseDescription courseDescription;
        IEnumerable<ICourseDescription> courseDescriptions;

        public Service()
        {
            courseDescriptions = _facade.FetchCourseDescriptions();
        }

        public string GetData(int courseNumber)
        {
            courseDescription = courseDescriptions.Where(c => c.CourseNumber.Equals(courseNumber)).FirstOrDefault();

            return courseDescription.Title;
        }
    }
}
