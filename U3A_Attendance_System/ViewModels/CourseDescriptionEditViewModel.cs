using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using U3A_Attendance_Model;
using U3A_Attendance_System.Views;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace U3A_Attendance_System.ViewModels
{
    public class CourseDescriptionEditViewModel : BaseViewModel
    {
        #region Properties
        private string _title;
        private string _description;

        public bool IsSavingEnabled { get; set; }

        [Required]
        public string Title
        {
            get { return _title; }

            [DebuggerNonUserCode]
            set
            {
                if (Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Title" }, null))
                {
                    _title = value;
                    NotifyOfPropertyChange("Title");
                    Validate();
                }
                if (_title != value)
                {
                    _title = value;
                    NotifyOfPropertyChange("Title");
                    Validate();
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Title" });
                }

            }
        }

        [Required]
        public string Description
        {
            get { return _description; }

            [DebuggerNonUserCode]
            set
            {
                if (Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Description" }, null))
                {
                    _description = value;
                    NotifyOfPropertyChange("Description");
                    Validate();
                }
                if (_description != value)
                {
                    _description = value;
                    NotifyOfPropertyChange("Description");
                    Validate();
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Description" });
                }

            }
        }


        public void Validate()
        {
            if (Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true))
            { IsSavingEnabled = true; this.Refresh(); }
            else
            { IsSavingEnabled = false; this.Refresh(); }
        }

        
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
                {
                    _facade.UpdateCourseDescription(CourseDescriptionProperties.Id, Title.Trim(), Description.Trim());
                    MessageBox.Show("Course Description successfully updated.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _facade.CreateCourseDescription(Title.Trim(), Description.Trim());
                    MessageBox.Show("Course Description was successfully created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
