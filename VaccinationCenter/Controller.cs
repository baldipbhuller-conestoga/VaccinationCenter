using System;
using System.Collections.Generic;
using System.Text;
using VaccinationCenter.Models;

namespace VaccinationCenter
{
    public class Controller
    {
        public bool RegisterAccount()
        {
            // dummy data insert
            int idResult = DBAccess.InsertAccount(new Account(Account.AccountTypes.User, "John1", "jnn20", "John", "Tim", "Martin", new DateTime(2000, 10, 22), "Toronto", "M4V2H5"));
            
            // returns true when ID is generated meaning not zero
            return (idResult != 0);
        }
    }
}
