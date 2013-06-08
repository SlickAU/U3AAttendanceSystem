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

        [Required(ErrorMessage="Required")]
        [RegularExpression(@"^(([a-zA-Z]*)(\s)([a-zA-Z]*))$", ErrorMessage="Name Surname")]
        public string Name
        {
            get { return _name; }

            [DebuggerNonUserCode]
            set
            {

                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Name" });
                _name = value;
                NotifyOfPropertyChange("Name");
            }
        }

        [Required(ErrorMessage = "Required")]
        public string Phone
        {
            get { return _phone; }

            [DebuggerNonUserCode]
            set
            {

                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Phone" });
                _phone = value;
                NotifyOfPropertyChange("Phone");
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

                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Email" });
                _email = value;
                NotifyOfPropertyChange("Email");
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
        
        public void Update()
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
