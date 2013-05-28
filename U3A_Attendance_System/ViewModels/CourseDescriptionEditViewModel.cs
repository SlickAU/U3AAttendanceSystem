using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;
using U3A_Attendance_System.Views;

namespace U3A_Attendance_System.ViewModels
{
    public class CourseDescriptionEditViewModel : BaseViewModel
    {
        #region Properties

        public string Title { get; set; }
        public string Description { get; set; }
        ICourseDescription _cd;

        #endregion


        public CourseDescriptionEditViewModel(ICourseDescription cd)
        {
            Title = cd.Title;
            Description = cd.Description;
            _cd = cd;
        }

        public CourseDescriptionEditViewModel()
        {

        }

        public ICourseDescription CourseDescriptionProperties { get { return _cd; } }

        public void Update()
        {
            if(ValuesNotNull())
            {
		        if (_cd != null)
                     _facade.UpdateCourseDescription(CourseDescriptionProperties.Id, Title.Trim(), Description.Trim());
                  else
                     _facade.CreateCourseDescription(Title.Trim(), Description.Trim());  
            }
        }

        public bool ValuesNotNull()
        {

            if(Title != null & Description != null)
            {
                return true;
            }

            throw new BusinessRuleException("Title and/or Description cannot be empty.");

        }

    }
}
