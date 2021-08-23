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

        public static List<Account> GetAllAccounts()
        {
            try
            {
                Account acc = null;
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Account]", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlConn.Open();

                // reads the result
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                List<Account> accountList = new List<Account>();

                // read all rows
                while (sqlDataReader.Read())
                {
                    acc = new Account();
                    acc.AccountID = sqlDataReader.GetInt32(0);
                    acc.AccountType = (Account.AccountTypes)Enum.Parse(typeof(Account.AccountTypes), sqlDataReader[1].ToString());
                    acc.Username = sqlDataReader[2].ToString();
                    acc.Password = sqlDataReader[3].ToString();

                    // null checks
                    if (!sqlDataReader.IsDBNull(4))
                    {
                        acc.FName = sqlDataReader[4].ToString();
                    }
                    if (!sqlDataReader.IsDBNull(5))
                    {
                        acc.MName = sqlDataReader[5].ToString();
                    }
                    if (!sqlDataReader.IsDBNull(6))
                    {
                        acc.LName = sqlDataReader[6].ToString();
                    }
                    if (!sqlDataReader.IsDBNull(7))
                    {
                        acc.Birthdate = sqlDataReader.GetDateTime(7);
                    }
                    if (!sqlDataReader.IsDBNull(8))
                    {
                        acc.City = sqlDataReader[8].ToString();
                    }
                    if (!sqlDataReader.IsDBNull(9))
                    {
                        acc.PostalCode = sqlDataReader[9].ToString();
                    }

                    accountList.Add(acc);
                }

                sqlConn.Close();

                return accountList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Clinic> GetAllClinics()
        {
            try
            {
                Clinic cl = null;
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Clinic]", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlConn.Open();

                // reads the result
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                List<Clinic> clinicList = new List<Clinic>();

                // read all rows
                while (sqlDataReader.Read())
                {
                    cl = new Clinic();
                    cl.ClinicID= sqlDataReader.GetInt32(0);
                    cl.LocationName = sqlDataReader[1].ToString();
                    cl.PostalCode = sqlDataReader[2].ToString();
                    cl.Capacity = sqlDataReader.GetInt32(3);

                    clinicList.Add(cl);

                }

                sqlConn.Close();

                return clinicList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int InsertClinic(Clinic cl)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT into [Clinic] VALUES (@LocationName,@PostalCode,@Capacity); SELECT SCOPE_IDENTITY()", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@LocationName", cl.LocationName);
                sqlCommand.Parameters.AddWithValue("@PostalCode", cl.PostalCode);
                sqlCommand.Parameters.AddWithValue("@Capacity", cl.Capacity);

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
            catch (Exception e)
            {
                throw e;
            }

        }

        public static int DeleteClinic(int id)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("DELETE FROM [Clinic] WHERE ClinicID = @ID", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@ID", id);

                sqlConn.Open();

                // return rows affected
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                sqlConn.Close();

                return rowsAffected;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static int InsertBooking(int accID, string referenceNumber, int clinicID, int doseType, string appDate, string appTime)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT into [Booking] VALUES (@AccountID,@ReferenceNumber,@ClinicID,@DoseType,@AppDate,@AppTime); SELECT SCOPE_IDENTITY()", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@AccountID", accID);
                sqlCommand.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                sqlCommand.Parameters.AddWithValue("@ClinicID", clinicID);
                sqlCommand.Parameters.AddWithValue("@DoseType", doseType);
                sqlCommand.Parameters.AddWithValue("@AppDate", appDate);
                sqlCommand.Parameters.AddWithValue("@AppTime", appTime);

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
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UpdateClinicCapacity(int id)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE [Clinic] SET Capacity = Capacity-1 WHERE ClinicID = @ID", sqlConn);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@ID", id);

                sqlConn.Open();

                sqlCommand.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
