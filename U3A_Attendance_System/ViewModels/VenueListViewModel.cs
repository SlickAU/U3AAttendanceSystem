using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class VenueListViewModel : BaseViewModel
    {
        #region Properties
        public IEnumerable<IVenue> VenueList
        {
            get { return _facade.FetchAllVenues(); }
        }
        #endregion


        #region Methods

        public void ShowVenueEdit(IVenue venue)
        {
            settings.Title = "Edit Venue";
            settings.SizeToContent = SizeToContent.Manual;
            _wm.ShowDialog(new VenueEditViewModel(venue), null, settings);
        }

        public void ShowVenueCreate()
        {
            settings.Title = "Create Venue";
            settings.SizeToContent = SizeToContent.Manual;

            _wm.ShowDialog(new VenueEditViewModel(), null, settings);
        }

        #endregion
    }
}
