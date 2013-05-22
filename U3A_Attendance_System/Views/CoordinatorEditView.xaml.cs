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

namespace U3A_Attendance_System.Views
{
    /// <summary>
    /// Interaction logic for CoordinatorEditView.xaml
    /// </summary>
    public partial class CoordinatorEditView : Window
    {
        public CoordinatorEditView()
        {
            InitializeComponent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
