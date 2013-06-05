using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U3A_Attendance_Model;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Collections.Specialized;

namespace U3A_Attendance_System.ViewModels
{

    public abstract class BaseViewModel : PropertyChangedBase, INotifyCollectionChanged
    {
        #region Members

        protected Facade _facade = FacadeFactory.Instance();
        protected AppWindowManager _wm = new AppWindowManager();
        protected dynamic settings = new ExpandoObject();

        #endregion

        #region Properties
   
        #endregion




        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
