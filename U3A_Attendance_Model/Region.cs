
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
internal partial class Region
{
	

	partial void ObjectPropertyChanged(string propertyName);


    public Region()
    {

        this.Suburbs = new HashSet<Suburb>();

        this.CourseInstances = new HashSet<CourseInstance>();

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

	private string _codeid;
    public  string CodeId 
		{ 
			get
			{ 
				return _codeid; 
			} 
			set
			{
				_codeid = value;
				ObjectPropertyChanged("CodeId");
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

	private System.Guid _u3aid;
    public  System.Guid U3AId 
		{ 
			get
			{ 
				return _u3aid; 
			} 
			set
			{
				_u3aid = value;
				ObjectPropertyChanged("U3AId");
			}
		}



    public virtual U3A U3A { get; set; }

    public virtual ICollection<Suburb> Suburbs { get; set; }

    public virtual ICollection<CourseInstance> CourseInstances { get; set; }

}

}
