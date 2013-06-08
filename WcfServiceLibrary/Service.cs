using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using U3A_Attendance_Model;
using U3A_Attendance_System;

namespace WcfServiceLibrary
{
    class Service : IService
    {
        //private Facade _facade = FacadeFactory.Instance();
        //private ICourseDescription courseDescription;
        //private IEnumerable<ICourseDescription> courseDescriptions;


        //public Service()
        //{
        //    courseDescriptions = _facade.FetchCourseDescriptions();
        //}

        //public string GetData(int courseNumber)
        //{
        //    courseDescription = courseDescriptions.Where(c => c.CourseNumber.Equals(courseNumber)).FirstOrDefault();

        //    return "TEst";
        //}
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
    }
}
