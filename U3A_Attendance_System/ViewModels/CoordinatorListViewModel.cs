using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;
using System.Windows;
using System.Collections.ObjectModel;

namespace U3A_Attendance_System.ViewModels
{
    public class CoordinatorListViewModel : BaseViewModel
    {
        private ObservableCollection<ICoordinator> coordinators;

        public ObservableCollection<ICoordinator> Coordinators
        {
            get
            {
                return new ObservableCollection<ICoordinator>(_facade.FetchCoordinators());
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
            settings.Title = "Delete Coordinator";
            _wm.ShowDialog(new DeleteViewModel(coordinator), null, settings);
            NotifyOfPropertyChange("Coordinators");
            //if (MessageBox.Show("Are you sure you want to delete this Coordinator?", "Confirm delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    _facade.DeleteCoordinator(coordinator.Id);

            //NotifyOfPropertyChange("Coordinators");
         }

        #endregion
    }
}
