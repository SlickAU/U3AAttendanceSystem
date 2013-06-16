using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using U3A_Attendance_Model;
using U3A_Attendance_System.Views;
using System.Windows.Forms;

namespace U3A_Attendance_System.ViewModels
{
    public class CourseInstanceEditViewModel : BaseViewModel
    {
        #region Fields
        private ICourseDescription _cd;
        private ICourseInstance _ci;
        private IEnumerable<IRegion> _regions;
        private IRegion _selectedRegion;
        private ISuburb _selectedSuburb;
        private IVenue _selectedVenue;
        private ILocation _selectedLocation;
        private ICoordinator _selectedCoordinator;
        private string _courseCode;
        private int _sessionOccurances;
        private int _selectedFrequency;

        private string[] _members;
        private Dictionary<Guid, string> _sessions = new Dictionary<Guid, string>();
        private string[,] _attendance;
        #endregion

        #region Properties
        //Course Instance Specific Properties

        public string CDTitle
        {
            get
            {
                return _cd.Title;
            }
            set {}
        }

        [Required(ErrorMessage="Required")]
        [RegularExpression(@"^(([1-9]{3})([A-Z]{2})([0-9]{2}))$", ErrorMessage="The Course Code must be in '138TB01' format")]
        public string CourseCode
        {
            get { return _courseCode; }

            [DebuggerNonUserCode]
            set
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "CourseCode" });
                _courseCode = value;
                IsUnsaved = "Visible";
                NotifyOfPropertyChange("CourseCode");
            }
        }

        public DateTime StartDate { get; set; }
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
                IsUnsaved = "Visible";
                NotifyOfPropertyChange("SelectedRegion");
            }
        }
        public IEnumerable<ISuburb> Suburbs { get; set; }
        public ISuburb SelectedSuburb
        {
            get
            {
                return _selectedSuburb;
            }
            set
            {
                _selectedSuburb = value;
                IsUnsaved = "Visible";
                NotifyOfPropertyChange("SelectedSuburb");
            }
        }
        public Boolean IsSuburbsEnabled
        {
            get
            {
                return !(SelectedRegion == null);
            }
        }
        public IEnumerable<IVenue> Venues { get; set; }
        public IVenue SelectedVenue
        {
            get
            {
                return _selectedVenue;
            }
            set
            {
                _selectedVenue = value;
                IsUnsaved = "Visible";
            }
        }
        public Boolean IsVenuesEnabled
        {
            get
            {
                return !(SelectedSuburb == null);
            }
        }
        public string IsUnsaved
        {
            get;
            set;
        }
        public IEnumerable<ILocation> Locations { get; set; }
        public ILocation SelectedLocation
        {
            get
            {
                return _selectedLocation;
            }
            set
            {
                _selectedLocation = value;
                IsUnsaved = "Visible";
                NotifyOfPropertyChange("SelectedLocation");
            }
        }
        public Boolean IsLocationsEnabled
        {
            get
            {
                return !(SelectedVenue == null || Locations.Count() <= 0);
            }
        }
        public Boolean VenueSelected
        {
            get
            {
                return true;
            }
        }
        private IEnumerable<ICoordinator> _coordinators;
        public IEnumerable<ICoordinator> Coordinators 
        { 
            get
            {
                return _coordinators;
            } 
            set
            {
                _coordinators = value;
            } 
        }
        public ICoordinator SelectedCoordinator
        {
            get
            {
                return _selectedCoordinator;
            }
            set
            {
                _selectedCoordinator = value;
                IsUnsaved = "Visible";
                NotifyOfPropertyChange("SelectedCoordinator");
            }
        }
        public string CIHash
        {
            get
            {
                if (_ci != null)
                {
                    MD5 hash = new MD5CryptoServiceProvider();

                    hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(_ci.Id.ToString()));

                    byte[] result = hash.Hash;

                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        strBuilder.Append(result[i].ToString("x2"));
                    }

                    return strBuilder.ToString();

                }
                return "Unable to compute hash code";
            }
        }
        public Boolean IsSessionsEditEnabled
        {
            get
            {
                return !(_ci == null);
            }
            set { }
        }

        //Session Specific Properties

        public string SaveOrUpdate
        {
            get;
            set;
        }

        private DateTime _sessionStartDate;
        public DateTime SessionStartDate
        {
            get
            {
                return _sessionStartDate;
            }
            set
            {
                _sessionStartDate = value;
                this.Refresh();
            }
        }
        public string SessionStartDateDay
        {
            get
            {
                return SessionStartDate.DayOfWeek.ToString();
            }
        }

        public Dictionary<string, int> SessionFrequency
        {
            get
            {
                var freqs = new Dictionary<string, int>();
                freqs.Add("Single", 0);
                freqs.Add("Weekly", 1);
                freqs.Add("Fortnightly", 2);
                freqs.Add("Monthly", 3);
                return freqs;
            }
        }

        public int SelectedFrequency
        {
            get
            {
                return _selectedFrequency;
            }
            set
            {
                _selectedFrequency = value;
                NotifyOfPropertyChange("SessionFrequency");
            }
        }

        [Required(ErrorMessage = "Number of Occurances is required")]
        //[RegularExpression(@"^([1-52])$", ErrorMessage = "Minimum number of occurances is 1, Maximum is 50")]
        public int SessionOccurances
        {
            get { return _sessionOccurances; }

            [DebuggerNonUserCode]
            set
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "SessionOccurances" });
                _sessionOccurances = value;
                NotifyOfPropertyChange("SessionOccurances");
            }
        }

        //public bool IsOccurancesEnabled
        //{
        //    get
        //    {
        //        return (SessionOccurances != 0);
        //    }

        //    set { }
        //}

        public IEnumerable<ISession> Sessions
        {
            get
            {
                if (_ci != null)
                    return _ci.Sessions;
                else
                    return null;
            }
            set {}
        }

        public bool IsAttendanceEnabled
        {
            get
            {
                return (_ci != null && _ci.Sessions.Count() > 0);
            }
            set { }
        }

        public IEnumerable<IMember> AttendanceMembers
        {
            get
            {
                try
                {
                    return Sessions.SelectMany(s => s.Attendances).Select(a => a.Member).Distinct().OrderBy(m => m.MemberId).ToList();
                }
                catch (Exception e)
                {

                }
                return null;
            }
        }

        public DataTable Attendances
        {
            get
            {
                if (AttendanceMembers != null && AttendanceMembers.Count() > 0)
                {
                    constructMembersArray(AttendanceMembers);
                    constructSessionsArray(Sessions);

                    string[,] arr = constructAttendanceArray();

                    DataTable dt = new DataTable();

                    dt.Columns.Add("Member #", typeof(string));

                    int cols = arr.GetLength(0);

                    for (int i = 0; i < cols; i++)
                    {
                        dt.Columns.Add(i.ToString(), typeof(string));
                    }

                    //Create Session Date columns (the first row)

                    var seshDates = _sessions.Values.ToArray();
                    string[] sessionDates = new string[seshDates.Length + 1];

                    sessionDates[0] = "Member No";

                    for (int j = 1; j < sessionDates.Length; j++)
                    {
                        sessionDates[j] = seshDates[j - 1];
                    }

                    dt.Rows.Add(sessionDates);

                    //Create Member # and Presence Rows (Preceeding rows)
                    for (int i = 0; i < arr.GetLength(1); i++)
                    {
                        string[] rowPresences = new string[arr.GetLength(0) + 1];
                        rowPresences[0] = _members[i];
                        //List<Object> rowPresences = new List<Object>();
                        //rowPresences.Add(_members[i]);
                        //string[] test = { 1000, "Y", "N", "N" };

                        for (int w = 1; w < sessionDates.Length; w++)
                        {
                            rowPresences[w] = arr[(w - 1), i];
                            //rowPresences.Add(new EditPresence(arr[(w - 1), i]));
                        }

                        dt.Rows.Add(rowPresences.ToArray());
                    }

                    return dt;
                }
                return null;
            }
        }
        #endregion

        #region Attendance Specific Properties

        public int MemberId
        {
            get;
            set;
        }
        private ISession _sessionOfAttendance;
        public ISession SessionOfAttendance
        {
            get
            {
                return _sessionOfAttendance;
            }
            set
            {
                _sessionOfAttendance = value;
            }
        }
        public IEnumerable<Presence> Presences { get { return Enum.GetValues(typeof(Presence)).OfType<Presence>(); } set { } }
        public Presence SelectedPresence { get; set; }
        
        #endregion

        #region Methods

        public CourseInstanceEditViewModel(ICourseDescription cd)
        {
            Regions = _facade.FetchRegions().ToList();
            Coordinators = _facade.FetchCoordinators().ToList();
            StartDate = DateTime.Now;
            SessionStartDate = DateTime.Now;
            SaveOrUpdate = "Save";
            _cd = cd;
        }

        public CourseInstanceEditViewModel(ICourseInstance ci)
        {
            Regions = _facade.FetchRegions().ToList();
            Suburbs = _facade.FetchSuburbsWithVenues(ci.RegionId);
            Venues = _facade.FetchVenues(ci.RegionId, ci.SuburbId);
            Locations = _facade.FetchLocations(ci.RegionId, ci.SuburbId, ci.VenueId);
            Coordinators = _facade.FetchCoordinators().ToList();
            SessionStartDate = DateTime.Now;

            _ci = ci;
            _cd = ci.CourseDescription;
            CourseCode = ci.CourseCode;
            StartDate = ci.StartDate;
            SelectedRegion = Regions.Where(reg => reg.Id.Equals(ci.RegionId)).FirstOrDefault();
            SelectedSuburb = Suburbs.Where(sub => sub.Id.Equals(ci.SuburbId)).FirstOrDefault();
            SelectedVenue = Venues.Where(ven => ven.Id.Equals(ci.VenueId)).FirstOrDefault();
            SelectedLocation = Locations.Where(loc => loc.Id.Equals(ci.DefaultLocationId)).FirstOrDefault();
            SelectedCoordinator = Coordinators.Where(co => co.Id.Equals(ci.TeacherId)).FirstOrDefault();
            IsUnsaved = "Hidden";
            SaveOrUpdate = "Update";
        }

        public void UpdateSuburbs(Guid regionId)
        {
            Suburbs = _facade.FetchSuburbsWithVenues(regionId);
            Venues = Enumerable.Empty<IVenue>();
            Locations = Enumerable.Empty<ILocation>();
            NotifyOfPropertyChange("Suburbs");
            this.Refresh();
        }

        public void UpdateVenues()
        {
            //SelectedRegion.Id = regionId;
            if ((SelectedSuburb != null) && !SelectedSuburb.Id.Equals(Guid.Empty))
            {
                Venues = _facade.FetchVenues(_selectedRegion.Id, _selectedSuburb.Id).ToList();
                this.Refresh();
            }
        }

        public void UpdateLocations(Guid venueId)
        {
            //SelectedVenueId = venueId;
            if (venueId != Guid.Empty)
            Locations = _facade.FetchLocations(_selectedRegion.Id, _selectedSuburb.Id, venueId).ToList();
            this.Refresh();
        }

        public void GenerateCourseCode()
        {
            if (SelectedVenue != null && SelectedRegion != null)
            {
                string courseCode;
                string semester;
                string year = StartDate.Year.ToString().Substring(2);
                int month = StartDate.Month;
                string region = SelectedRegion.CodeId;
                string venue = SelectedVenue.CodeId;

                if (month <= 6)
                {
                    semester = "1";
                }

                else
                {
                    semester = "2";
                }

                courseCode = string.Format("{0}{1}{2}{3}", year, semester, region, venue);

                string result = _facade.CheckCourseCode(courseCode, SelectedRegion.Id, SelectedVenue.Id);

                CourseCode = result;
                this.Refresh();
            }
        }

        public void Save()
        {
            ICourseInstance ci;

            CheckForNullValues();

            if (_ci == null)
            {
                ci = _facade.CreateCourseInstance(_cd.Id, _selectedCoordinator.Id, _selectedRegion.Id, _selectedSuburb.Id, _selectedVenue.Id, _selectedLocation.Id, StartDate, CourseCode);
                System.Windows.Forms.MessageBox.Show("Course Instance was successfully created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ci = _facade.UpdateCourseInstance(_ci.Id, _selectedCoordinator.Id,_selectedRegion.Id, _ci.RegionId, _selectedSuburb.Id, _selectedVenue.Id, _selectedLocation.Id, StartDate, _ci.StateId, CourseCode);
                System.Windows.Forms.MessageBox.Show("Course Instance was successfully Updated.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            _ci = ci;

            _facade.SerializeObject(ci);
            IsUnsaved = "Hidden";
            NotifyOfPropertyChange("IsUnsaved");
            this.Refresh();
        }

        public void CheckForNullValues()
        {

            if (SelectedCoordinator == null)
            {
                throw new BusinessRuleException("Coordinator cannot be Empty");
            }

            if (SelectedRegion == null)
            {
                throw new BusinessRuleException("Region cannot be Empty");
            }

            if (SelectedSuburb == null)
            {
                throw new BusinessRuleException("Suburb cannot be Empty");
            }

            if (SelectedVenue == null)
            {
                throw new BusinessRuleException("Venue cannot be Empty");
            }

            if (SelectedLocation == null)
            {
                throw new BusinessRuleException("Location cannot be Empty");
            }

            if (StartDate == null)
            {
                throw new BusinessRuleException("StartDate cannot be Empty");
            }

            if (CourseCode == null)
            {
                throw new BusinessRuleException("CourseCode cannot be Empty");
            }
        }
        
        //Session Management
        public void CreateSession(int frequency)
        {
            try
            {
                if (frequency != null && SessionOccurances != null)
                {
                    DateTime newSession = SessionStartDate;

                    if (frequency == 0)
                    {
                        SessionOccurances = 1;
                    }

                    for (int i = 0; i < SessionOccurances; i++)
                    {
                        if (SessionOccurances == 1 && frequency != 0)
                        {
                            _facade.CreateSession(_ci.Id, _ci.RegionId, SelectedSuburb.Id, SelectedVenue.Id, _ci.DefaultLocationId, SessionStartDate);
                            break;
                        }

                        if (frequency == 0)
                        {
                            _facade.CreateSession(_ci.Id, _ci.RegionId, SelectedSuburb.Id, SelectedVenue.Id, _ci.DefaultLocationId, SessionStartDate);
                            break;
                        }

                        else
                        {
                            switch (frequency)
                            {
                                case 1:
                                    //Week
                                    if (SessionOccurances > 52 || 1 > SessionOccurances)
                                    {
                                        throw new BusinessRuleException("Weekly occurances have to be between 1 and 52!");
                                    }
                                    else
                                    {
                                        newSession = newSession.AddDays(7);
                                        break;
                                    }
                                case 2:
                                    //Fortnightly
                                    if (SessionOccurances > 26 || 1 > SessionOccurances)
                                    {
                                        throw new BusinessRuleException("Fortnightly occurances have to be between 1 and 26!");
                                    }
                                    else
                                    {
                                        newSession = newSession.AddDays(14);
                                        break;
                                    }
                                case 3:
                                    //Monthly
                                    if (SessionOccurances > 12 || 1 > SessionOccurances)
                                    {
                                        throw new BusinessRuleException("Monthly occurances have to be between 1 and 12!");
                                    }
                                    else
                                    {
                                        newSession = newSession.AddMonths(1);
                                        break;
                                    }
                            }

                            _facade.CreateSession(_ci.Id, _ci.RegionId, SelectedSuburb.Id, SelectedVenue.Id, _ci.DefaultLocationId, newSession);
                        }
                    }
                    
                    System.Windows.Forms.MessageBox.Show("The Session(s) were succesfully created!", "Success", MessageBoxButtons.OK);
                }
              
                this.Refresh();
            }
            catch (BusinessRuleException e)
            {
                System.Windows.Forms.MessageBox.Show( e.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public void DeleteSession(ISession session)
        {
            try
            {
                if (session != null)
                {
                    settings.Title = "Delete Session";
                    _wm.ShowDialog(new DeleteViewModel((ISession)session), null, settings);
                    this.Refresh();
                }
                else
                {
                    throw new BusinessRuleException("A session must be selected in order to delete");
                }
            }
            catch (BusinessRuleException e)
            {              
                System.Windows.Forms.MessageBox.Show( e.Message, "Error", MessageBoxButtons.OK);
            }
        }

        //Attendance Management

        public void AddAttendance()
        {
            _facade.CreateAttendance(_ci.RegionId, _ci.Id, SessionOfAttendance.Id, MemberId, SelectedPresence.ToString());
            this.Refresh();

            /*foreach (ISession sesh in Sessions)
            {
                _facade.CreateAttendance(_ci.RegionId, _ci.Id, sesh.Id, MemberId, "");
            }*/
            
        }

        private void constructMembersArray(IEnumerable<IMember> members)
        {
            _members = new string[members.Count()];

            for (int i = 0; i < _members.Length; i++)
            {
                _members[i] = members.ElementAt(i).MemberId.ToString();
            }
        }

        private void constructSessionsArray(IEnumerable<ISession> sessions)
        {
            for (int i = 0; i < sessions.Count(); i++)
            {
                _sessions[sessions.ElementAt(i).Id] = sessions.ElementAt(i).Date.ToShortDateString();
            }
        }


        private string[,] constructAttendanceArray()
        {
            _attendance = new string[_sessions.Count(), _members.Count()];

            for (int i = 0; i < _sessions.Count(); i++)
            {
                for (int j = 0; j < _members.Length; j++)
                {
                    //_attendance[i, j] = Sessions.Where(s => s.Id.Equals(_sessions.ElementAt(i).Key)).SelectMany(a => a.Attendances).Where(m => m.MemberId.Equals(_members[j])).Select(m => m.Presence).SingleOrDefault().ToString();
                    ISession session = Sessions.Where(s => s.Id.Equals(_sessions.ElementAt(i).Key)).FirstOrDefault();
                    var attendance = session
                        .Attendances
                        .Where(m => m.MemberId.Equals(Convert.ToInt32 (_members[j]))).FirstOrDefault();
                    _attendance[i, j] = (attendance != null) ? attendance.Presence : "N";
                }
            }

            return _attendance;

        }

        public void ShowCISessionEdit(ISession session)
        {
            try
            {
                if(session != null)
                {
                    settings.Title = "Edit Session";
                    _wm.ShowDialog(new CourseInstanceSessionEditViewModel(session), null, settings);
                    this.Refresh();  
                }
        
                else
                {
                    throw new BusinessRuleException("A session must be selected in order to edit");
                }
            }
        
            catch (BusinessRuleException e)
            {              
                System.Windows.Forms.MessageBox.Show( e.Message, "Error", MessageBoxButtons.OK);
            }
        }


        public void AddNewCoordinator()
        {
            settings.Title = "Create Coordinator";
            _wm.ShowDialog(new CoordinatorEditViewModel(), null, settings);
             Coordinators = _facade.FetchCoordinators().ToList();
             NotifyOfPropertyChange("Coordinators");
        }

        public void AddNewVenue()
        {
            settings.Title = "Create Venue";
            _wm.ShowDialog(new VenueEditViewModel(), null, settings);
            UpdateSuburbs(_selectedRegion.Id);
            NotifyOfPropertyChange("Venues");
        }

        public void AddNewLocation()
        {
            settings.Title = "Add Location";
            if (_selectedVenue == null)
            {
                throw new BusinessRuleException("Please select a venue first");
            }
            _wm.ShowDialog(new VenueEditViewModel(_selectedVenue), null, settings);
            Locations = _facade.FetchLocations(_selectedRegion.Id, _selectedSuburb.Id, _selectedVenue.Id);
            NotifyOfPropertyChange("Locations");
        }

        #endregion

    }
}
