using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        private string _venueName;
        private string _address;
        private string _codeId;
        private string _roomName;
        public bool IsSavingEnabled { get; set; }
        public bool IsAddingEnabled { get; set; }
        public bool IsDeletingEnabled { get; set; }


        [Required(ErrorMessage = "Required")]
        public string VenueName
        {
            get { return _venueName; }

            [DebuggerNonUserCode]
            set
            {
                if (Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "VenueName" }, null))
                {
                    _venueName = value;
                    NotifyOfPropertyChange("VenueName");
                    ValidateVenue();
                }

                if (_venueName != value)
                {                    
                    _venueName = value;
                    NotifyOfPropertyChange("VenueName");
                    ValidateVenue();
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "VenueName" });
                }
                
            }
        }

        [Required(ErrorMessage = "Required")]
        public string Address
        {
            get { return _address; }

            [DebuggerNonUserCode]
            set
            {
                if (Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Address" }, null))
                {
                   
                    _address = value;
                    NotifyOfPropertyChange("Address");
                    ValidateVenue();
                }
                if (_address != value)
                {
                    _address = value;
                    NotifyOfPropertyChange("Address");
                    ValidateVenue();
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Address" });
                }
            }
        }

        [Required(ErrorMessage = "Required"), MaxLength(1, ErrorMessage="Code Id must be 1 character")]
        public string CodeId
        {
            get { return _codeId; }

            [DebuggerNonUserCode]
            set
            {
                if (Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "CodeId" }, null))
                {
                    _codeId = value;
                    NotifyOfPropertyChange("CodeId");
                    ValidateVenue();
                }
                if (_codeId != value)
                {
                    _codeId = value;
                    NotifyOfPropertyChange("CodeId");
                    ValidateVenue();
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "CodeId" });
                }
            }
        }


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
                ValidateVenue();
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
                ValidateVenue();
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
            ListOfLocations = new BindingList<ILocation>(existingVenue.Locations.ToList());
            Rooms = ListOfLocations.Select(l => l.Room).ToList();
            SelectedRegion = Regions.Where(r => r.Id.Equals(existingVenue.RegionId)).FirstOrDefault();
            Suburbs = _facade.FetchSuburbs(SelectedRegion.Id);
            SelectedSuburb = Suburbs.Where(s => s.Id.Equals(existingVenue.SuburbId)).FirstOrDefault();
            IsDeletingEnabled = true;
            this.Refresh();
        }
        //--------------------------------------
        #endregion

        #region Methods

        #region Location Manegement
        //--------------------------------------
        private IEnumerable<ILocation> _listOfLocations;
        public BindingList<ILocation> ListOfLocations
        {
            get {
                if (existingVenue != null)
                    return new BindingList<ILocation>(_facade.FetchLocations(existingVenue.RegionId, existingVenue.SuburbId, existingVenue.Id).ToList());
                else
                    return null;
            }
            set { }
        }
        public List<string> Rooms { get; set; }

        [Required(ErrorMessage="Required")]
        public string RoomName
        {
            get { return _roomName; }

            [DebuggerNonUserCode]
            set
            {
                if (Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "RoomName" }, null))
                {
                    _roomName = value;
                    NotifyOfPropertyChange("RoomName");
                    ValidateRoom();
                }
                if (_roomName != value)
                { 
                    _roomName = value;
                    NotifyOfPropertyChange("RoomName");
                    ValidateRoom();
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "RoomName" });
                }
            }
        }

        public Boolean IsLocationManagementEnabled
        {
            get { return !(existingVenue == null); }
        }

        public void DeleteLocation(Guid locationId)
        {
            if (locationId != Guid.Empty)
            {
                _facade.DeleteLocation(SelectedRegion.Id, SelectedSuburb.Id, existingVenue.Id, locationId);
                //  ListOfLocations.ToList().Remove(ListOfLocations.Where(l => l.Id.Equals(locationId)).FirstOrDefault());
                NotifyOfPropertyChange("ListOfLocations");
                if (ListOfLocations.Count == 0)
                    IsDeletingEnabled = false;
                this.Refresh();
            }
            else
                throw new BusinessRuleException("Please select a location");
        }


        public void ClearListOfLocations()
        {
           int count = ListOfLocations.Count();
           var locations = ListOfLocations;
            //Not working properly e.g. leaves some of the locations undeleted from first try
           for (int i = 0; i < count; i++)
           {
                _facade.DeleteLocation(SelectedRegion.Id, SelectedSuburb.Id, existingVenue.Id, locations[i].Id);
           }

           NotifyOfPropertyChange("ListOfLocations");
           if (ListOfLocations.Count == 0)
               IsDeletingEnabled = false;
           this.Refresh();

        }

        public void UpdateLocations()
        {
            /*List<ILocation> list = new List<ILocation>();

            if (existingVenue.Locations.Count() == 0)
            {
                list.Add(_facade.CreateLocation(_selectedRegion.Id, existingVenue.SuburbId, existingVenue.Id, RoomName));
            }
            else
            {
                if (RoomName != null)
                {
                    list = ListOfLocations.ToList();
                    list.Add(_facade.CreateLocation(_selectedRegion.Id, existingVenue.SuburbId, existingVenue.Id, RoomName));
                }
            }

            ListOfLocations = list.AsEnumerable();



            this.Refresh();*/

            if (RoomName != null)
                ListOfLocations.Add(_facade.CreateLocation(_selectedRegion.Id, existingVenue.SuburbId, existingVenue.Id, RoomName));
            IsDeletingEnabled = true;
            NotifyOfPropertyChange("ListOfLocations");
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
       
            //if (Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true))
            //{
                if (existingVenue != null)
                    existingVenue = _facade.UpdateVenue(existingVenue.Id, _selectedRegion.Id, _selectedSuburb.Id, VenueName.Trim(), Address.Trim(), CodeId.Trim());
                else
                    existingVenue = _facade.CreateVenue(_selectedRegion.Id, _selectedSuburb.Id, VenueName.Trim(), Address.Trim(), CodeId.Trim());

                this.Refresh();
            //}
        }

        //--------------------------------------
        #endregion

        #endregion

        public class ValidateVenueProperties
        {
            [Required]
            public string _VenueName { get; set; }
            [Required]
            public string _Address { get; set; }
            [Required]
            public string _CodeId {get;set;}
            [Required]
            public IRegion _SelectedRegion { get; set; }
            [Required]
            public ISuburb _SelectedSuburb { get; set; }
           
        }

        public class ValidateLocationProperties
        {
            [Required]
            public string _RoomName { get; set; }
        }


        public void ValidateVenue()
        {
            ValidateVenueProperties testObject = new ValidateVenueProperties() { _VenueName = VenueName, _Address = Address, _CodeId = CodeId, _SelectedRegion = SelectedRegion, _SelectedSuburb = SelectedSuburb };

            if (Validator.TryValidateObject(testObject, new ValidationContext(testObject, null, null), null, true))
            { IsSavingEnabled = true; this.Refresh(); }
            else
            { IsSavingEnabled = false; this.Refresh(); }
        }

        public void ValidateRoom()
        {
            ValidateLocationProperties testObject = new ValidateLocationProperties() { _RoomName = RoomName };

            if (Validator.TryValidateObject(testObject, new ValidationContext(testObject, null, null), null, true))
            { IsAddingEnabled = true; this.Refresh(); }
            else
            { IsAddingEnabled = false; this.Refresh(); }
        }
    }
}
