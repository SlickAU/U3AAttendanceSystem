using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    internal partial class CourseDescription : ICourseDescription
    {
        public CourseDescription(string title, string description, Guid u3aId)
        {
            Title = title;
            Description = description;
            U3AId = u3aId;
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
    }
}
