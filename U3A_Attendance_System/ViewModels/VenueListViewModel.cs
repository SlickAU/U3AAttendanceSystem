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

        public void Delete(IVenue venue)
        {
            if (MessageBox.Show("Are you sure you want to delete this venue?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    _facade.DeleteVenue(venue.Id, venue.RegionId, venue.SuburbId);
                }
                catch (InvalidOperationException e)
                {
                    if (MessageBox.Show("Warning! This venue has corresponding locations, deleting this venue will also delete application, continue?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
            
                        var locations = venue.Locations;
                        
                       

                        for (int i = 0; i < locations.Count(); i++)
                        {
                            _facade.DeleteLocation(venue.RegionId, venue.SuburbId, venue.Id, locations.ElementAt(i).Id);
                        }

                        _facade.DeleteVenue(venue.Id, venue.RegionId, venue.SuburbId);
                    }
                }

                this.Refresh();
            }
        }
        #endregion
    }
}
