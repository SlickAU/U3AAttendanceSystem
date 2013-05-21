using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class CoordinatorListViewModel : BaseViewModel
    {
        private IEnumerable<ICoordinator> coordinators;

        public IEnumerable<ICoordinator> Coordinators
        {
            get
            {
                if (coordinators == null)
                {
                    return _facade.FetchCoordinators();
                }

                else
                {
                    return coordinators;
                }
            }
            set
            {
                coordinators = value; 
            }
        }
    }
}
