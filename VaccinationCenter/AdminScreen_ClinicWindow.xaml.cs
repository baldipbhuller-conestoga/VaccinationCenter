using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VaccinationCenter
{
    /// <summary>
    /// Interaction logic for AdminScreen_ClinicWindow.xaml
    /// </summary>
    public partial class AdminScreen_ClinicWindow : Window
    {
        public AdminScreen_ClinicWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
