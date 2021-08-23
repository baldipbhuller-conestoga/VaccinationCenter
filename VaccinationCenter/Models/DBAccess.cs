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

        public static Account RetrieveAccount(string username, string password, string role)
        {
            try
            {
                Account acc = null;
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Account] WHERE Username = @Username AND Password = @Password AND AccountType = @AccountType", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@Username", username);
                sqlCommand.Parameters.AddWithValue("@Password", password);
                sqlCommand.Parameters.AddWithValue("@AccountType", role);

                sqlConn.Open();

                // returns the id of the data row added if successful, else returns null for failed insert
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                // only read 1 row
                if (sqlDataReader.Read())
                {
                    acc = new Account();
                    acc.AccountID = sqlDataReader.GetInt32(0);
                    acc.AccountType = (Account.AccountTypes) Enum.Parse(typeof(Account.AccountTypes), sqlDataReader[1].ToString());
                    acc.Username = sqlDataReader[2].ToString();
                    acc.Password = sqlDataReader[3].ToString();
                    acc.FName = sqlDataReader[4].ToString();
                    acc.MName = sqlDataReader[5].ToString();
                    acc.LName = sqlDataReader[6].ToString();
                    acc.Birthdate = sqlDataReader.GetDateTime(7);
                    acc.City = sqlDataReader[8].ToString();
                    acc.PostalCode = sqlDataReader[9].ToString();
                }

                sqlConn.Close();

                return acc;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
