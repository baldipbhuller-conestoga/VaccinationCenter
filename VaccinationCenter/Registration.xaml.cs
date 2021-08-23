using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        Controller controller;

        public Registration()
        {
            InitializeComponent();
            controller = Controller.getInstance();
        }

        private void btn_loginClick(object sender, RoutedEventArgs e)
        {
            LoginScreen dashboard = new LoginScreen();
            dashboard.Show();
            this.Close();
        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int registerResult = controller.RegisterAccount(comboSelectionRole.Text,txtUsername.Text,txtPassword.Password,txtFirstName.Text,"",txtLastName.Text,DateTime.Now,"","");

                if (registerResult >= 1)
                {
                    MessageBox.Show("Account Registerd Successfully", "Register", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (registerResult == -1)
                {
                    MessageBox.Show("Username already existing", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Account Register Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Somthing went wrong " + ex.Message); 
            }
        }

        private void confirm_Passward(object sender, TextCompositionEventArgs e)
        {

        }
    
    }
}
