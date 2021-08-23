using System;
using System.Collections.Generic;
using System.Text;
using VaccinationCenter.Models;

namespace VaccinationCenter
{
    public class Controller
    {
        // This is for singleton pattern having only one instance of controller
        private static Controller controller = new Controller();

        private Account loggedinAccount;

        // Do not allow controlle to be created in other classes
        private Controller()
        {

        }
        
        public static Controller getInstance()
        {
            return controller;
        }

        public int LoginAccount(string username, string password, string role)
        {
            Account acc = DBAccess.RetrieveAccount(username, password, role);

            if(acc != null)
            {
                loggedinAccount = acc;

                if(loggedinAccount.AccountType == Account.AccountTypes.User)
                {
                    return 1;
                }
                else if (loggedinAccount.AccountType == Account.AccountTypes.Admin)
                {
                    return 2;
                }
            }

            return 0;
        }

        public bool RegisterAccount(string accountType, string username, string password, string fName, string mName, string lName, DateTime dt, string city, string postalCode)
        {
            Account acc = new Account((Account.AccountTypes)Enum.Parse(typeof(Account.AccountTypes), accountType),
                                      username,password,fName,mName,lName,dt,city,postalCode);
            
            int idResult = DBAccess.InsertAccount(acc);
            
            // returns true when ID is generated meaning not zero
            return (idResult != 0);
        }
    }
}
