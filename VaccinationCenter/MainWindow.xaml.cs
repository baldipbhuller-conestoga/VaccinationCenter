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
using VaccinationCenter.Models;

namespace VaccinationCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Account acc = new Account();
        public MainWindow()
        {
            InitializeComponent();
            txtUserName.Text = acc.Username;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_InsertRecods(object sender, RoutedEventArgs e)
        {

        }
        public void clearData()
        {
            txtBirthDate.DisplayDateStart = DateTime.Today;
            comboBox_dose.SelectedIndex=-1;
            comboBox_dose.SelectedIndex=-1;
        }

        private void btn_clear(object sender, RoutedEventArgs e)
        {
            clearData();
        }
    }
}
