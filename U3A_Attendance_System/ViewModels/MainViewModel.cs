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
            if (MessageBox.Show("Really delete?", "Confirm delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _facade.DeleteCourseDescription(cd.Id);


        }

        public void ShowTabbedView()
        {
            ActivateItem(new TabbedViewModel());
        }

        public void ShowVenueEdit(IVenue venue)
        {
            settings.Title = "Edit Venue";
            settings.SizeToContent = SizeToContent.Manual;
            _wm.ShowDialog(new VenueEditViewModel(venue), null, settings);
        }

        public void ShowVenueCreate()
        {
            settings.Title = "Create Venue";
           // settings.SizeToContent = SizeToContent.Manual;


            _wm.ShowDialog(new VenueEditViewModel(), null, settings);
        }

    }
}
