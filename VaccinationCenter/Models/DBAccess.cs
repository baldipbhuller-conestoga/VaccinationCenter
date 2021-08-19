using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Data;

namespace VaccinationCenter.Models
{
    public class DBAccess
    {
        // Connection string in app.config
        private static SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);
        
        public static int InsertAccount(Account acc)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT into [Account] VALUES (@AccountType,@Username,@Password,@Fname,@Mname,@Lname,@Birthdate,@City,@PostalCode); SELECT SCOPE_IDENTITY()", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@AccountType", acc.AccountType.ToString());
                sqlCommand.Parameters.AddWithValue("@Username", acc.Username);
                sqlCommand.Parameters.AddWithValue("@Password", acc.Password);
                sqlCommand.Parameters.AddWithValue("@Fname", acc.FName);
                sqlCommand.Parameters.AddWithValue("@Mname", acc.MName);
                sqlCommand.Parameters.AddWithValue("@Lname", acc.LName);
                sqlCommand.Parameters.AddWithValue("@Birthdate", acc.Birthdate.ToString("yyyy-MM-dd"));
                sqlCommand.Parameters.AddWithValue("@City", acc.City);
                sqlCommand.Parameters.AddWithValue("@PostalCode", acc.PostalCode);

                sqlConn.Open();

                // returns the id of the data row added if successful, else returns null for failed insert
                object idReturned = sqlCommand.ExecuteScalar();
                int id = 0;

                if (idReturned != null)
                {
                    id = Convert.ToInt32(idReturned);
                }

                sqlConn.Close();

                return id;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
