
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
internal partial class Suburb
{
	

	partial void ObjectPropertyChanged(string propertyName);


    public Suburb()
    {

        this.Venues = new HashSet<Venue>();

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

	private string _name;
    public  string Name 
		{ 
			get
			{ 
				return _name; 
			} 
			set
			{
				_name = value;
				ObjectPropertyChanged("Name");
			}
		}

	private int _postcode;
    public  int PostCode 
		{ 
			get
			{ 
				return _postcode; 
			} 
			set
			{
				_postcode = value;
				ObjectPropertyChanged("PostCode");
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



    public virtual Region Region { get; set; }

    public virtual ICollection<Venue> Venues { get; set; }

}

}
