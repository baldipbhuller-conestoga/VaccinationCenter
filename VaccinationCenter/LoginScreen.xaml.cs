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
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            
            Application.Current.Shutdown();
        }

        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=LAPTOP-J7TM7QA2\SQLEXPRESS19;Initial Catalog=Conestoga1;Integrated Security=True");
            try
            {
                if (sqlcon.State == System.Data.ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sqlcommands = new SqlCommand("SELECT * FROM [userINfo] WHERE userName=@Username AND Password=@Password", sqlcon);
                sqlcommands.CommandType = CommandType.Text;
                sqlcommands.Parameters.AddWithValue("@Username", txtUserName.Text);
                sqlcommands.Parameters.AddWithValue("@Password", TxtPassword.Text);
                int count = Convert.ToInt32(sqlcommands.ExecuteScalar());

                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();

                }
                if (count == 2)
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
            catch (Exception ex) { MessageBox.Show("Somthing went wrong " + ex.Message); }
        }
    }
}
