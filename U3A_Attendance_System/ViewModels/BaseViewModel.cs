using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using U3A_Attendance_System.Validation;

namespace U3A_Attendance_System.ViewModels
{

    public abstract class BaseViewModel : ValidatingScreen<BaseViewModel>
    {
        #region Members

        protected Facade _facade = FacadeFactory.Instance();

        #endregion

        #region Properties
   
        #endregion

    }
}
