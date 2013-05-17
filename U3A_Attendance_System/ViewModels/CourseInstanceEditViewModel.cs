using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using U3A_Attendance_Model;

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

        private string[] _members;
        private Dictionary<Guid, string> _sessions = new Dictionary<Guid, string>();
        private string[,] _attendance;
        #endregion

        #region Properties
        //Course Instance Specific Properties

        public string CDTitle { get; set; }
        public string CourseCode { get; set; }
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
                NotifyOfPropertyChange("SelectedVenue");
            }
        }
        public Boolean IsVenuesEnabled
        {
            get
            {
                return !(SelectedSuburb == null);
            }
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
        public IEnumerable<ICoordinator> Coordinators { get; set; }
        public ICoordinator SelectedCoordinator
        {
            get
            {
                return _selectedCoordinator;
            }
            set
            {
                _selectedCoordinator = value;
                NotifyOfPropertyChange("SelectedCoordinator");
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

        public IEnumerable<ISession> Sessions
        {
            get
            {
                //var sessions = _ci.Sessions ?? null;
                /*_ci.Sessions.SelectMany(s => s.Attendances); 
                var attendances = sessions.SelectMany(s => s.Attendances).ToList();
                var members = attendances.Select(a => a.Member).Distinct().ToList();
                int width = sessions.Count();
                int height = members.Count();

                var m = _ci.Sessions.SelectMany(s => s.Attendances).Select(a => a.Member).Distinct().ToList();

                var x = m.ToList();

                //if (_ci != null)
                //{
                //    var sessions = _facade.FetchSessions(_ci.Id, _ci.RegionId);
                //    return (sessions == null) ? null : sessions;
                //}*/
                //return sessions ?? null;
                if (_ci != null)
                    return _ci.Sessions;
                else
                    return null;
            }
            set {}
        }

        public IEnumerable<IMember> AttendanceMemberNos
        {
            get
            {
                return Sessions.SelectMany(s => s.Attendances).Select(a => a.Member).Distinct().OrderBy(m => m.MemberId).ToList();
            }
        }

        public DataTable Attendances
        {
            get
            {
                constructMembersArray(AttendanceMemberNos);
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
                    //string[] test = { 1000, "Y", "N", "N" };

                    for (int w = 1; w < rowPresences.Length; w++)
                    {
                        rowPresences[w] = arr[(w -1), i];
                    }

                    dt.Rows.Add(rowPresences);
                }

                return dt;
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

            _cd = cd;
            CDTitle = cd.Title;
        }

        public CourseInstanceEditViewModel(ICourseInstance ci)
        {
            Regions = _facade.FetchRegions().ToList();
            Suburbs = _facade.FetchSuburbs(ci.RegionId);
            Venues = _facade.FetchVenues(ci.RegionId, ci.SuburbId);
            Locations = _facade.FetchLocations(ci.RegionId, ci.SuburbId, ci.VenueId);
            Coordinators = _facade.FetchCoordinators().ToList();
            SessionStartDate = DateTime.Now;

            _ci = ci;
            //CISessions = ci.CourseSessions;
            CourseCode = ci.CourseCode;
            StartDate = ci.StartDate;
            SelectedRegion = Regions.Where(reg => reg.Id.Equals(ci.RegionId)).FirstOrDefault();
            SelectedSuburb = Suburbs.Where(sub => sub.Id.Equals(ci.SuburbId)).FirstOrDefault();
            SelectedVenue = Venues.Where(ven => ven.Id.Equals(ci.VenueId)).FirstOrDefault();
            SelectedLocation = Locations.Where(loc => loc.Id.Equals(ci.DefaultLocationId)).FirstOrDefault();
            SelectedCoordinator = Coordinators.Where(co => co.Id.Equals(ci.CoordinatorId)).FirstOrDefault();
        }

        public void UpdateSuburbs(Guid regionId)
        {
            Suburbs = _facade.FetchSuburbsWithVenues(regionId);
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
            Locations = _facade.FetchLocations(_selectedRegion.Id, _selectedSuburb.Id, venueId).ToList();
            this.Refresh();
        }

        public void GenerateCourseCode()
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

            _facade.CheckCourseCode(courseCode);

            CourseCode = courseCode;
            this.Refresh();
        }

        public void Save()
        {
            ICourseInstance ci;

            if(_ci == null)
                 ci = _facade.CreateCourseInstance(_cd.Id, _selectedCoordinator.Id, _selectedRegion.Id, _selectedSuburb.Id, _selectedVenue.Id, _selectedLocation.Id, StartDate, CourseCode);
            else
                 ci = _facade.UpdateCourseInstance(_ci.Id, _selectedCoordinator.Id, _selectedRegion.Id, _selectedSuburb.Id, _selectedVenue.Id, _selectedLocation.Id, StartDate, _ci.StateId, CourseCode);

            _ci = ci;
            this.Refresh();
        }


        //Session Management

        public void CreateSession()
        {
            _facade.CreateSession(_ci.Id, _ci.RegionId, SelectedSuburb.Id, SelectedVenue.Id, _ci.DefaultLocationId, SessionStartDate);
            _ci = _facade.FetchCourseInstance(_ci.Id, _ci.RegionId);
            this.Refresh();
        }

        //Attendance Management

        public void AddAttendance()
        {
            _facade.CreateAttendance(_ci.RegionId, _ci.Id, SessionOfAttendance.Id, MemberId, SelectedPresence.ToString());

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

        

        #endregion

    }
}
