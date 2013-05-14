using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace U3A_Attendance_Model
{
    public class BusinessRuleException : Exception
    {

        public BusinessRuleException(string message) : base(message) { }
    }
}
