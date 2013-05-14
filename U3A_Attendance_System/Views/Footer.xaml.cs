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

namespace U3A_Attendance_System
{
    /// <summary>
    /// Interaction logic for MasterPage.xaml
    /// </summary>
    public partial class Footer : Page
    {
        public Footer()
        {
            InitializeComponent();
        }

        private void Home_btn_Click(object sender, RoutedEventArgs e)
        {
            var view = new CourseDescriptionList();
            DuhPump.MainFrame.Content = view;
        }
    }
}
