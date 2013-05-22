using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;
using U3A_Attendance_System.Views;
using System.Dynamic;
using System.Windows;

namespace U3A_Attendance_System.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        //private WindowManager _wm = new WindowManager();
        Facade _facade = FacadeFactory.Instance();
        private AppWindowManager _wm = new AppWindowManager();
        dynamic settings = new ExpandoObject();


        public MainViewModel()
        {
            ShowTabbedView();
        }
        
        public void ShowCDList()
        {
            ActivateItem(new CourseDescriptionListViewModel());
        }

        public void ShowCIList(ICourseDescription cd)
        {
            ActivateItem(new CourseDescriptionInstancesViewModel(cd));
        }

        public void ShowCDEdit(ICourseDescription cd)
        {
            settings.Title = "Edit course description";
            settings.SizeToContent = SizeToContent.Manual;

            _wm.ShowDialog(new CourseDescriptionEditViewModel(cd), null, settings);
        }

        //TODO: For some reason it can distinquish between paramater types...
        public void ShowCIEdit(Object obj)
        {
            if(obj is ICourseDescription)
                ActivateItem(new CourseInstanceEditViewModel((ICourseDescription)obj));
            else
                ActivateItem(new CourseInstanceEditViewModel((ICourseInstance)obj));
        }

        public void ShowCDEdit()
        {
            settings.Title = "Create course description";
            settings.SizeToContent = SizeToContent.Manual;


            _wm.ShowDialog(new CourseDescriptionEditViewModel(), null, settings);
        }

        public void DeleteDescriptionConfirm(ICourseDescription cd)
        {
            if (MessageBox.Show("Are you sure you want to delete this Course?", "Confirm delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _facade.DeleteCourseDescription(cd.Id);
        }

        public void ShowTabbedView()
        {
            ActivateItem(new TabbedViewModel());
        }

        public void ShowVenueEdit(IVenue venue)
        {
            settings.Title = "Edit Venue";
            _wm.ShowDialog(new VenueEditViewModel(venue), null, settings);
        }

        public void ShowVenueCreate()
        {
            settings.Title = "Create Venue";
            _wm.ShowDialog(new VenueEditViewModel(), null, settings);
        }

        public void ShowCoordinatorEdit(ICoordinator coordinator)
        {
            settings.Title = "Edit Coordinator";
            _wm.ShowDialog(new CoordinatorEditViewModel(coordinator), null, settings);
        }

        public void ShowCoordinatorCreate()
        {
            settings.Title = "Create Coordinator";
            _wm.ShowDialog(new CoordinatorEditViewModel(), null, settings);
        }

        public void ShowCoordinatorDelete(ICoordinator coordinator)
        {
            if (MessageBox.Show("Are you sure you want to delete this Coordinator?", "Confirm delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _facade.DeleteCoordinator(coordinator.Id);
        }

        public void ShowCISessionEdit(ISession session)
        {
            settings.Title = "Edit Session";
            _wm.ShowDialog(new CourseInstanceSessionEditViewModel(session), null, settings);
        }
    }
}
