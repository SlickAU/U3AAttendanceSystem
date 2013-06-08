using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U3A_Attendance_Model;

namespace U3AWeb.Models
{
    public class HomeModel
    {
        Facade _facade = new Facade(true);

        public IEnumerable<ICourseDescription> CourseDescriptions;

        public HomeModel()
        {
            CourseDescriptions = _facade.FetchCourseDescriptions();
        }
    }
}