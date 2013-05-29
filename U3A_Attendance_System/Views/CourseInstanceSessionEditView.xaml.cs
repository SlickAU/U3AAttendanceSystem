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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace U3A_Attendance_System.Views
{
    /// <summary>
    /// Interaction logic for CourseInstanceSessionEditView.xaml
    /// </summary>
    public partial class CourseInstanceSessionEditView : MetroWindow
    {
        public CourseInstanceSessionEditView()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
