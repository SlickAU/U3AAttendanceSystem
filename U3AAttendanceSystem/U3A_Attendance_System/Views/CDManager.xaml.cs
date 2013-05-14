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

namespace U3A_Attendance_System.Views
{
    /// <summary>
    /// Interaction logic for CDManager.xaml
    /// </summary>
    public partial class CDManager : UserControl
    {
        public CDManager()
        {
            InitializeComponent();
            DataContext = new CourseDescriptionEditModel();
        }

        private void Update_btn(object sender, RoutedEventArgs e)
        {

        }
    }
}
