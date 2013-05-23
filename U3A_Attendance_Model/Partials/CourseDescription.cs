using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model.Interfaces;

namespace U3A_Attendance_Model
{
    internal partial class CourseDescription : ICourseDescription, ISearchable
    {
        public bool HasInstances
        {
            get
            {
                return CourseInstances.Count > 0;
            }
        }

        public CourseDescription(string title, string description, Guid u3aId)
        {
            Title = title;
            Description = description;
            U3AId = u3aId;
            CourseInstances = new List<CourseInstance>();
        }

        #region CourseDescription Management

        //Updates a CourseDescription
        internal CourseDescription update(string title, string description)
        {
            Title = title;
            Description = description;

            return this;
        }

        //Deletes a CourseDescription
        internal void delete(Action<CourseDescription> action)
        {
            if (this.CourseInstances.Count > 0)
            {
                throw new BusinessRuleException("Course Description cannot be deleted as there are Course Instances associated.");
            }

            action(this);
        }

        internal IEnumerable<CourseInstance> fetchCourseInstances()
        {
            var value = CourseInstances.AsEnumerable();

            if (value.Count().Equals(0))
            {
                throw new BusinessRuleException("Could not obtain collection of Course Instances");
            }

            return value;
        }

        #endregion
    
        public bool MeetsCritera(string keyword)
        {
            
            if(Description.ToLower().Trim().Contains(keyword))
            {
                return true; 
            }
            if(Title.ToLower().Trim().Contains(keyword))
            {
                return true; 
            }

 	       return false; 
        }


        IEnumerable<ICourseInstance> ICourseDescription.CourseInstances
        {
            get { return CourseInstances; }
        }
    }
}
