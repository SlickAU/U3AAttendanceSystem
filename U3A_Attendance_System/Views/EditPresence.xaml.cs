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
using U3A_Attendance_Model;

namespace U3A_Attendance_System.Views
{
    /// <summary>
    /// Interaction logic for EditPresence.xaml
    /// </summary>
    public partial class EditPresence : UserControl
    {
        public EditPresence(string p)
        {
            InitializeComponent();

            Presence.Content = p.ToString();
        }
    }
}
