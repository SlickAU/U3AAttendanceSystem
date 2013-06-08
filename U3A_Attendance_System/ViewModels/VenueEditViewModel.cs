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


        [Required(ErrorMessage = "Required")]
        public string VenueName
        {
            get { return _venueName; }

            [DebuggerNonUserCode]
            set
            {

                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "VenueName" });
                _venueName = value;
                NotifyOfPropertyChange("VenueName");
            }
        }

        [Required(ErrorMessage = "Required")]
        public string Address
        {
            get { return _address; }

            [DebuggerNonUserCode]
            set
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Address" });
                _address = value;
                NotifyOfPropertyChange("Address");
            }
        }

        [Required(ErrorMessage = "Required"), MaxLength(1, ErrorMessage="Code Id must be 1 character")]
        public string CodeId
        {
            get { return _codeId; }

            [DebuggerNonUserCode]
            set
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "CodeId" });
                _codeId = value;
                NotifyOfPropertyChange("CodeId");
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
            ListOfLocations = new BindingList<ILocation>(existingVenue.Locations.ToList());
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

                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "RoomName" });
                _roomName = value;
                NotifyOfPropertyChange("RoomName");
            }
        }

        public Boolean IsLocationManagementEnabled
        {
            get { return !(existingVenue == null); }
        }

        public void DeleteLocation(Guid locationId)
        {
            _facade.DeleteLocation(SelectedRegion.Id, SelectedSuburb.Id, existingVenue.Id, locationId);
         //  ListOfLocations.ToList().Remove(ListOfLocations.Where(l => l.Id.Equals(locationId)).FirstOrDefault());
            NotifyOfPropertyChange("ListOfLocations");
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
                _facade.CreateLocation(_selectedRegion.Id, existingVenue.SuburbId, existingVenue.Id, RoomName);

            NotifyOfPropertyChange("ListOfLocations");
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
            if (Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true))
            {
                if (existingVenue != null)
                    existingVenue = _facade.UpdateVenue(existingVenue.Id, _selectedRegion.Id, _selectedSuburb.Id, VenueName.Trim(), Address.Trim(), CodeId.Trim());
                else
                    existingVenue = _facade.CreateVenue(_selectedRegion.Id, _selectedSuburb.Id, VenueName.Trim(), Address.Trim(), CodeId.Trim());

                this.Refresh();
            }
        }

        //--------------------------------------
        #endregion

        #endregion
    }
}
