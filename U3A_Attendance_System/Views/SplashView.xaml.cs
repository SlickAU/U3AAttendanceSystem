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
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace U3A_Attendance_System.Views
{
    /// <summary>
    /// Interaction logic for SplashView.xaml
    /// </summary>
    public partial class SplashView : Window
    {
        public SplashView()
        {
            //this.Owner = currentWindow;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Stream imgStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("U3A_Attendance_System.loading.gif");

            this.pictureBoxLoading.Image =  new Bitmap(imgStream);
        }
    }
}
