using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
        Controller accController;
        public MainWindow()
        {
            InitializeComponent();
            txtUserName.Text = accController.LoggedinAccount.Username;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

       
        public void LoadGrid()
        {
            accController.LoadClinics();

            DataTable dataTable = ToDataTable(accController.BookAppointment);

            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private DataTable ToDataTable(Func<string, int, DateTime, int> bookAppointment)
        {
            throw new NotImplementedException();
        }

        

        public bool isValid()
        {
            
            if (txtBirthDate.Text == string.Empty)
            {
                MessageBox.Show("Date of birth is Required", "Missing info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (comboBox_dose.SelectedIndex == -1)
            {
                MessageBox.Show("Dosage is Required", "Missing info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (txtTimeSlot.Text == string.Empty)
            {
                MessageBox.Show("Time slot is not selected !", "Missing info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        public void clearData()
        {
            txtBirthDate.Text = "";
            comboBox_dose.SelectedIndex=-1;
            comboBox_dose.SelectedIndex=-1;
        }

        private void btn_clear(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void bookappointment_btn(object sender, RoutedEventArgs e)
        {

        }
    }
}
