﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public class AssociationDependencyException : BusinessRuleException
    {
        public AssociationDependencyException(string message)
            : base(message)
        {

        }
    }
}
