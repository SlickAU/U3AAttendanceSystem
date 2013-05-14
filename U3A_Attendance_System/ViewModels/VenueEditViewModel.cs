using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System.ViewModels
{
    public class VenueEditViewModel : BaseViewModel
    {
        private IEnumerable<IRegion> _regions;
        private ISuburb _selectedSuburb;
        private IRegion _selectedRegion;

        public string VenueName { get; set; }

        public string Address { get; set; }

        public string CodeId { get; set; }

        public IEnumerable<IVenue> Venues { get; set; }

        public Boolean IsVenuesEnabled
        {
            get
            {
                return !(SelectedSuburb == null);
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


        public VenueEditViewModel()
        {
            Regions = _facade.FetchRegions().ToList();
        }

        public VenueEditViewModel(IVenue venue)
        {
            Regions = _facade.FetchRegions().ToList();
            VenueName = venue.Name;
            Address = venue.Address;
            CodeId = venue.CodeId;
            //Bind the region and suburb for the venue
        }


        public void UpdateSuburbs(Guid regionId)
        {
            Suburbs = _facade.FetchSuburbs(regionId);
            this.Refresh();
        }

        //public void UpdateVenues()
        //{
        //    if ((SelectedSuburb != null) && !SelectedSuburb.Id.Equals(Guid.Empty))
        //    {                
        //        this.Refresh();
        //    }
        //}

        public void Update()
        {
            _facade.CreateVenue(_selectedRegion.Id, _selectedSuburb.Id, VenueName, Address, CodeId);
        }
    }
}
