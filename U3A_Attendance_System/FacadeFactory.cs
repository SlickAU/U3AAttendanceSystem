using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;

namespace U3A_Attendance_System
{
    public class FacadeFactory
    {
        private static Facade _facade;

        /// <summary>
        /// Empty FacadeFactory Constructor.
        /// </summary>
        protected FacadeFactory()
        {

        }

        /// <summary>
        /// Initalises static _facade field.
        /// </summary>
        /// <returns>_facade</returns>
        public static Facade Instance()
        {
            if (_facade == null)
                _facade = new Facade();

            return _facade;
        }
    }
}
