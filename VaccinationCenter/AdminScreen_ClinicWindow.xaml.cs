using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using VaccinationCenter.Models;

namespace VaccinationCenter
{
    /// <summary>
    /// Interaction logic for AdminScreen_ClinicWindow.xaml
    /// </summary>
    public partial class AdminScreen_ClinicWindow : Window
    {
        Controller controller;

        public AdminScreen_ClinicWindow()
        {
            InitializeComponent();
            controller = Controller.getInstance();
            LoadGrid();
        }

        public void LoadGrid()
        {
            controller.LoadClinics();

            DataTable dataTable = ToDataTable(controller.ClinicList);

            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        public DataTable ToDataTable(List<Clinic> items)
        {
            DataTable dataTable = new DataTable(typeof(Clinic).Name);
            PropertyInfo[] Props = typeof(Clinic).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                // Set columns
                dataTable.Columns.Add(prop.Name);
            }
            foreach (Clinic item in items)
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
            int num;
            if (txtLocationName.Text == string.Empty)
            {
                MessageBox.Show("Location Name is Required", "Missing info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (txtPostalCode.Text == string.Empty)
            {
                MessageBox.Show("Postal Code is Required", "Missing info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (txtTotalCapacity.Text == string.Empty || !int.TryParse(txtTotalCapacity.Text,out num))
            {
                MessageBox.Show("Capacity is Required Or is Incorrect", "Missing info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private void InserButn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {

                    int addResult = controller.AddClinic(txtLocationName.Text, txtPostalCode.Text, Convert.ToInt32(txtTotalCapacity.Text));

                    if (addResult >= 1)
                    {
                        LoadGrid();
                        MessageBox.Show("Clinic Added Successfully", "Add Clinic", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (addResult == -1)
                    {
                        MessageBox.Show("Clinic name already existing", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Clinic Add Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Somthing went wrong " + ex.Message);
            }
        }

        private void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num;

                if (isValid() && !string.IsNullOrEmpty(txtID.Text) && int.TryParse(txtID.Text,out num))
                {

                    int updateResult = controller.UpdateClinic(txtLocationName.Text, txtPostalCode.Text, Convert.ToInt32(txtTotalCapacity.Text), Convert.ToInt32(txtID.Text));

                    if (updateResult == 1)
                    {
                        LoadGrid();
                        MessageBox.Show("Clinic Updated Successfully", "Clinic Update", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Clinic Update Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int num;

                if (!string.IsNullOrEmpty(txtID.Text) && int.TryParse(txtID.Text, out num))
                {

                    if (controller.RemoveClinic(Convert.ToInt32(txtID.Text)))
                    {
                        LoadGrid();
                        MessageBox.Show("Clinic Removed Successfully", "Clinic Remove", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Clinic Remove Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtLocationName.Clear();
            txtPostalCode.Clear();
            txtTotalCapacity.Clear();
            txtID.Clear();
        }
    }
}
