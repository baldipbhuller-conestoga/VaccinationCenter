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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        Controller controller;

        public LoginScreen()
        {
            InitializeComponent();
            controller = Controller.getInstance();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int loginResult = controller.LoginAccount(txtUserName.Text, txtPassword.Text, cmbRole.Text);

                if (loginResult == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else if (loginResult == 2)
                {
                    AdminScreen_ClinicWindow dashboard = new AdminScreen_ClinicWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong username and password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Somthing went wrong " + ex.Message);
            }
            
        }
    }
}
