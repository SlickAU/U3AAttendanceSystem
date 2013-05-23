using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;
using System.Windows;

namespace U3A_Attendance_System.ViewModels
{
    public class CoordinatorListViewModel : BaseViewModel
    {
        private IEnumerable<ICoordinator> coordinators;

        public BindingList<ICoordinator> Coordinators
        {
            get
            {
                /*if (coordinators == null)
                {
                    return _facade.FetchCoordinators();
                }

                else
                {
                    return coordinators;
                }*/
                return new BindingList<ICoordinator>(new List<ICoordinator>(_facade.FetchCoordinators()));
            }
            set { }
        }

        #region Methods

        public void ShowCoordinatorEdit(ICoordinator coordinator)
        {
            settings.Title = "Edit Coordinator";
            _wm.ShowDialog(new CoordinatorEditViewModel(coordinator), null, settings);
            
            NotifyOfPropertyChange("Coordinators");
        }
 
        public void ShowCoordinatorCreate()
        {
            settings.Title = "Create Coordinator";
            _wm.ShowDialog(new CoordinatorEditViewModel(), null, settings);

            NotifyOfPropertyChange("Coordinators");
        }
 
        public void ShowCoordinatorDelete(ICoordinator coordinator)
        {
            if (MessageBox.Show("Are you sure you want to delete this Coordinator?", "Confirm delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _facade.DeleteCoordinator(coordinator.Id);

            NotifyOfPropertyChange("Coordinators");
         }

        #endregion
    }
}
