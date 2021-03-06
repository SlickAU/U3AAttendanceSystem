﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public interface ILocation
    {
        Guid Id { get; }
        string Room { get; }
        Guid VenueId { get; }
        IVenue Venue { get; }
    }
}
