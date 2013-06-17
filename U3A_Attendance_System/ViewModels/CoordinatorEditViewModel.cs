using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class CoordinatorEditViewModel : BaseViewModel
    {
        private string _name;
        private string _phone;
        private string _email;
        public bool IsSavingEnabled { get; set; }

        [Required(ErrorMessage="Required")]
        [RegularExpression(@"^([A-Za-z'\-\p{L}\p{Zs}\p{Lu}\p{Ll}\']+)$", ErrorMessage = "Name Surname")]
        public string Name
        {
            get { return _name; }

            [DebuggerNonUserCode]
            set
            {

                if (Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Name" }, null))
                {
                    _name = value;
                    NotifyOfPropertyChange("Name");
                    Validate();
                }
                if (_name != value)
                {
                    _name = value;
                    NotifyOfPropertyChange("Name");
                    Validate();
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Name" });
                }
            }
        }

      [Required(ErrorMessage = "Required")]
        public string Phone
        {
            get { return _phone; }

            [DebuggerNonUserCode]
            set
            {

                if (Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Phone" }, null))
                {
                    _phone = value;
                    NotifyOfPropertyChange("Phone");
                    Validate();
                }
                if (_phone != value)
                {
                    _phone = value;
                    NotifyOfPropertyChange("Phone");
                    Validate();
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Phone" });
                }
            }
        }




        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email: email@email.com")]
        public string Email
        {
            get { return _email; }

            [DebuggerNonUserCode]
            set
            {

                if (Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Email" }, null))
                {

                    _email = value;
                    NotifyOfPropertyChange("Email");
                    Validate();
                }
                if (_email != value)
                {
                    _email = value;
                    NotifyOfPropertyChange("Email");
                    Validate();
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Email" });
                }
              
            }
        }

        ICoordinator _coo;
        
        public ICoordinator CoordinatorProperties { get { return _coo; } }             
        
        public CoordinatorEditViewModel()
        {

        }

        public CoordinatorEditViewModel(ICoordinator coordinator)
        {
            Name = coordinator.Name;
            Phone = coordinator.Phone;
            Email = coordinator.Email;
            _coo = coordinator;
        }

        public bool Validate()
        {                      
            if (Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true))
            { IsSavingEnabled = true; this.Refresh(); return true; }
            else
            { IsSavingEnabled = false; this.Refresh(); return false; }
 
        }


        public void Update()
        {
            if (Validate() == true)
            {
                if (_coo != null)
                {
                    _facade.UpdateCoordinator(CoordinatorProperties.Id, Name, Email, Phone);
                }
                else
                {
                    _facade.CreateCoordinator(Name, Email, Phone);
                }
            }
        }
    }
}
