//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace U3A_Attendance_Model
{
    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel;
    internal partial class CourseInstance
    {
    	
    	partial void ObjectPropertyChanged(string propertyName);
    
        public CourseInstance()
        {
            this.Sessions = new HashSet<Session>();
        }
    
    	private System.Guid _id;
        public  System.Guid Id 
		{ 
			get
			{ 
				return _id; 
			} 
			set
			{
				_id = value;
				ObjectPropertyChanged("Id");
			}
		}
    	private System.DateTime _startdate;
        public  System.DateTime StartDate 
		{ 
			get
			{ 
				return _startdate; 
			} 
			set
			{
				_startdate = value;
				ObjectPropertyChanged("StartDate");
			}
		}
    	private System.Guid _courseid;
        public  System.Guid CourseId 
		{ 
			get
			{ 
				return _courseid; 
			} 
			set
			{
				_courseid = value;
				ObjectPropertyChanged("CourseId");
			}
		}
    	private System.Guid _regionid;
        public  System.Guid RegionId 
		{ 
			get
			{ 
				return _regionid; 
			} 
			set
			{
				_regionid = value;
				ObjectPropertyChanged("RegionId");
			}
		}
    	private System.Guid _coordinatorid;
        public  System.Guid CoordinatorId 
		{ 
			get
			{ 
				return _coordinatorid; 
			} 
			set
			{
				_coordinatorid = value;
				ObjectPropertyChanged("CoordinatorId");
			}
		}
    	private int _stateid;
        public  int StateId 
		{ 
			get
			{ 
				return _stateid; 
			} 
			set
			{
				_stateid = value;
				ObjectPropertyChanged("StateId");
			}
		}
    	private System.Guid _defaultlocationid;
        public  System.Guid DefaultLocationId 
		{ 
			get
			{ 
				return _defaultlocationid; 
			} 
			set
			{
				_defaultlocationid = value;
				ObjectPropertyChanged("DefaultLocationId");
			}
		}
    	private string _coursecode;
        public  string CourseCode 
		{ 
			get
			{ 
				return _coursecode; 
			} 
			set
			{
				_coursecode = value;
				ObjectPropertyChanged("CourseCode");
			}
		}
    
        public virtual Coordinator Coordinator { get; set; }
        public virtual CourseDescription CourseDescription { get; set; }
        public virtual Location Location { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
