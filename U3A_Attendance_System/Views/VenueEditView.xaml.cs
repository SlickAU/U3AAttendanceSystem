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
using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace U3A_Attendance_System.Views
{
    /// <summary>
    /// Interaction logic for VenueEditView.xaml
    /// </summary>
    public partial class VenueEditView : MetroWindow
    {
        public VenueEditView()
        {
            InitializeComponent();
            ConventionManager.ApplyValidation = (binding, viewModelType, property) => binding.ValidatesOnExceptions = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
