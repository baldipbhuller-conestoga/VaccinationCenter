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
            accController = Controller.getInstance();
            LoadGrid();  
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


       
        public void LoadGrid()
        {
            accController.LoadBooking();

            DataTable dataTable = ToDataTable(accController.BookingList);

            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        public DataTable ToDataTable(List<Booking> items)
        {
            DataTable dataTable = new DataTable(typeof(Booking).Name);
            PropertyInfo[] Props = typeof(Booking).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                // Set columns
                dataTable.Columns.Add(prop.Name);
            }
            foreach (Booking item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }




        public bool isValid()
        {
            
            if (txtBirthDate.Text == string.Empty)
            {
                MessageBox.Show("Date of birth is Required", "Missing info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (txtDose.Text == string.Empty)
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
            txtLocation.Text = "";
            txtDose.Text = "";
            
        }

        private void btn_clear(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void bookappointment_btn(object sender, RoutedEventArgs e)
        {
                     
            accController.BookAppointment(txtLocation.Text, int.Parse(txtDose.Text), DateTime.Parse(txtTimeSlot.Text), appointmentDate.SelectedDate.Value);
            LoadGrid();
        }

        private void delete_btn(object sender, StylusEventArgs e)
        {
            try
            {
                int num;

                if (!string.IsNullOrEmpty(txtID.Text) && int.TryParse(txtID.Text, out num))
                {

                    if (accController.RemoveClinic(Convert.ToInt32(txtID.Text)))
                    {
                        LoadGrid();
                        MessageBox.Show("Booking Removed Successfully", "Clinic Remove", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Booking Remove Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("ID is Required", "Missing info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Somthing went wrong " + ex.Message);
            }
        }
    }
}
