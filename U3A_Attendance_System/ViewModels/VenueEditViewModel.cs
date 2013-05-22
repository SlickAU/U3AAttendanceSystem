﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class VenueEditViewModel : BaseViewModel
    {
        #region Fields
        //--------------------------------------
        private IEnumerable<IRegion> _regions;
        private ISuburb _selectedSuburb;
        private IRegion _selectedRegion;
        private ILocation _selectedLocation;
        private IVenue existingVenue;
        //--------------------------------------
        #endregion

        #region Properties
        //--------------------------------------
        public string VenueName { get; set; }
        public string Address { get; set; }
        public string CodeId { get; set; }
        public IEnumerable<ISuburb> Suburbs { get; set; }
        public Boolean IsSuburbsEnabled
        {
            get
            {
                return !(SelectedRegion == null);
            }
        }
        public ISuburb SelectedSuburb
        {
            get
            {
                return _selectedSuburb;
            }
            set
            {
                _selectedSuburb = value;
                NotifyOfPropertyChange("SelectedSuburb");
            }
        }
        public IEnumerable<IRegion> Regions
        {
            get { return _regions; }
            set
            {
                NotifyOfPropertyChange("Regions");
                _regions = value;
            }
        }
        public IRegion SelectedRegion
        {
            get
            {
                return _selectedRegion;
            }
            set
            {
                _selectedRegion = value;
                NotifyOfPropertyChange("SelectedRegion");
            }
        }
        //--------------------------------------
        #endregion

        #region Constructors
        //--------------------------------------
        public VenueEditViewModel()
        {
            Regions = _facade.FetchRegions().ToList();
        }
        public VenueEditViewModel(IVenue venue)
        {
            existingVenue = venue;
            VenueName = existingVenue.Name;
            Address = existingVenue.Address;
            CodeId = existingVenue.CodeId;
            Regions = _facade.FetchRegions().ToList();
            ListOfLocations = existingVenue.Locations;
            Rooms = ListOfLocations.Select(l => l.Room).ToList();
            SelectedRegion = Regions.Where(r => r.Id.Equals(existingVenue.RegionId)).FirstOrDefault();
            Suburbs = _facade.FetchSuburbs(SelectedRegion.Id);
            SelectedSuburb = Suburbs.Where(s => s.Id.Equals(existingVenue.SuburbId)).FirstOrDefault();
        }
        //--------------------------------------
        #endregion

        #region Methods

        #region Location Manegement
        //--------------------------------------
        private IEnumerable<ILocation> _listOfLocations;
        public IEnumerable<ILocation> ListOfLocations
        {
            get { return _listOfLocations; }
            set { _listOfLocations = value; this.Refresh(); }
        }
        public List<string> Rooms { get; set; }
        public string RoomName { get; set; }
        public Boolean IsLocationManagementEnabled
        {
            get { return !(existingVenue == null); }
        }

        public void DeleteLocation(Guid locationId)
        {
            _facade.DeleteLocation(SelectedRegion.Id, SelectedSuburb.Id, existingVenue.Id, locationId);
         //  ListOfLocations.ToList().Remove(ListOfLocations.Where(l => l.Id.Equals(locationId)).FirstOrDefault());
            this.Refresh();
        }

        public void ClearListOfLocations()
        {
            //Not working properly e.g. leaves some of the locations undeleted from first try
           for (int i = 0; i < ListOfLocations.Count(); i++)
           {
                _facade.DeleteLocation(SelectedRegion.Id, SelectedSuburb.Id, existingVenue.Id, ListOfLocations.ToArray()[i].Id);
                this.Refresh();
            }
           
        }

        public void UpdateLocations()
        {
            //List<ILocation> list = new List<ILocation>();

            //if (RoomName != null)
            //{
            //    //list.Add(RoomName);
            //    //ListOfLocations = list;
            //    list.Add(_facade.CreateLocation(_selectedRegion.Id, existingVenue.SuburbId, existingVenue.Id, RoomName));
            //}

            //ListOfLocations = list.AsEnumerable();

            _listOfLocations.ToList().Add(_facade.CreateLocation(_selectedRegion.Id, existingVenue.SuburbId, existingVenue.Id, RoomName));
            Rooms = _listOfLocations.Select(l => l.Room).ToList();


            this.Refresh();
        }
        //--------------------------------------
        #endregion

        #region Venue Management
        //--------------------------------------
        public void UpdateSuburbs(Guid regionId)
        {
            Suburbs = _facade.FetchSuburbs(regionId);
            this.Refresh();
        }
        public void Update()
        {
            existingVenue = _facade.CreateVenue(_selectedRegion.Id, _selectedSuburb.Id, VenueName, Address, CodeId);
            this.Refresh();
        }

        //--------------------------------------
        #endregion

        #endregion
    }
}
