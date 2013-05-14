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

namespace U3A_Attendance_System.Views
{
    /// <summary>
    /// Interaction logic for CourseDescriptionList.xaml
    /// </summary>
    public partial class CourseDescriptionListView : UserControl, IScreen
    {

        public CourseDescriptionListView()
        {
            InitializeComponent();

            //DataContext = new CourseDescriptionListModel();
        }
        private void Edit_btn(object sender, RoutedEventArgs e)
        {

            //var courseDesc = ((Button)e.OriginalSource).DataContext as ICourseDescription;
            //var view = new CourseDescriptionEdit(courseDesc);
            //DuhPump.MainFrame.Navigate(view.Content);
           

        }

        private void View_btn(object sender, RoutedEventArgs e)
        {
            //var courseDesc = ((Button)e.OriginalSource).DataContext as ICourseDescription;
            //var view = new CourseDescriptionInstances(courseDesc.Id);
            //DuhPump.MainFrame.Navigate(view.Content);
        }

        private void Create_btn(object sender, RoutedEventArgs e)
        {
            /*var courseDesc = ((Button)e.OriginalSource).DataContext as ICourseDescription;
            var view = new CourseInstanceEdit(courseDesc);
            DuhPump.MainFrame.Content = view;*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //DuhPump.MainFrame.Content = new CourseDescriptionEdit();
        }


        private void Delete_btn(object sender, RoutedEventArgs e)
        {
            /*var courseDesc = ((Button)e.OriginalSource).DataContext as ICourseDescription;

            facade.DeleteDescription(courseDesc);*/
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
