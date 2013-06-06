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
using U3A_Attendance_Model;

namespace U3A_Attendance_System.Views
{
    /// <summary>
    /// Interaction logic for CourseDescriptionList.xaml
    /// </summary>
    public partial class CourseDescriptionListView : UserControl
    {

        public CourseDescriptionListView()
        {
            InitializeComponent();

            //DataContext = new CourseDescriptionListModel();
        }
        private void Edit_btn(object sender, RoutedEventArgs e)
        {

            //var courseDesc = ((Button)e.OriginalSource).DataContext as ICourseDescription;

            //(DataContext as CourseDescriptionListViewModel).ShowCDEdit(courseDesc);
            //var view = new CourseDescriptionEdit(courseDesc);
            //DuhPump.MainFrame.Navigate(view.Content);           
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

 
    }
}
