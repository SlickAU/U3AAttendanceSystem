using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using U3A_Attendance_System.ViewModels;
using Caliburn.Micro;
using System.Windows.Interactivity;

namespace U3A_Attendance_System.Views
{
    /// <summary>
    /// Interaction logic for CourseDescriptionEditView.xaml
    /// </summary>
    public partial class CourseDescriptionEditView : UserControl, IScreen
    {
        public CourseDescriptionEditView()
        {
            InitializeComponent();
            
        }

        private void Update_btn(object sender, RoutedEventArgs e)
        {
            

            /*var facade = new U3A_Attendance_Model.Facade();

            if (_course == null)
                facade.AddCourse(courseName_txt.Text, description_txt.Text);
            else
                facade.UpdateCourse(_course.Id, courseName_txt.Text, description_txt.Text);*/

        }

        public string DisplayName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ActivationEventArgs> Activated;

        public bool IsActive
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<DeactivationEventArgs> AttemptingDeactivation;

        public void Deactivate(bool close)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<DeactivationEventArgs> Deactivated;

        public void CanClose(Action<bool> callback)
        {
            throw new NotImplementedException();
        }

        public void TryClose()
        {
            throw new NotImplementedException();
        }

        public bool IsNotifying
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void NotifyOfPropertyChange(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
