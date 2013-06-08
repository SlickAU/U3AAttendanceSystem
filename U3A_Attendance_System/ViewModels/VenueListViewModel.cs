﻿using System;
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
        public DoubleLinkedList<IVenue> VenueList
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
            NotifyOfPropertyChange("VenueList");
        }

        public void ShowVenueCreate()
        {
            settings.Title = "Create Venue";
            settings.SizeToContent = SizeToContent.Manual;

            _wm.ShowDialog(new VenueEditViewModel(), null, settings);
            NotifyOfPropertyChange("VenueList");
        }

        public void Delete(IVenue venue)
        {
            if (MessageBox.Show("Are you sure you want to delete this venue?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    _facade.DeleteVenue(venue.Id, venue.RegionId, venue.SuburbId);
                }
                catch (AssociationDependencyException e)
                {
                    if (MessageBox.Show("Warning! This venue has corresponding locations, deleting this venue will also delete all corresponding locations, continue?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
            
                        var locations = venue.Locations.AsEnumerable();
                        int count = locations.Count();

                        for (int i = 0; i < count; i++)
                        {
                            _facade.DeleteLocation(venue.RegionId, venue.SuburbId, venue.Id, locations.ElementAt(0).Id);
                        }

                        _facade.DeleteVenue(venue.Id, venue.RegionId, venue.SuburbId);
                    }
                }

            }
            NotifyOfPropertyChange("VenueList");
        }
        #endregion
    }
}
