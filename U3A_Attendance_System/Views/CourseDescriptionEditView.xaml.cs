﻿using System;
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
    public partial class CourseDescriptionEditView : Window
    {
        public CourseDescriptionEditView()
        {
            InitializeComponent();     
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
