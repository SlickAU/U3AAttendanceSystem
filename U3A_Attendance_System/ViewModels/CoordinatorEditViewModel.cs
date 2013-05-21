using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class CoordinatorEditViewModel : BaseViewModel
    {

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
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
