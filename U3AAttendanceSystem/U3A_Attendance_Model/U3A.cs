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
    internal partial class U3A
    {
    	
    	partial void ObjectPropertyChanged(string propertyName);
    
        public U3A()
        {
            this.Coordinators = new HashSet<Coordinator>();
            this.CourseDescriptions = new HashSet<CourseDescription>();
            this.Members = new HashSet<Member>();
            this.Regions = new HashSet<Region>();
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
    	private string _description;
        public  string Description 
		{ 
			get
			{ 
				return _description; 
			} 
			set
			{
				_description = value;
				ObjectPropertyChanged("Description");
			}
		}
    
        public virtual ICollection<Coordinator> Coordinators { get; set; }
        public virtual ICollection<CourseDescription> CourseDescriptions { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
    }
}