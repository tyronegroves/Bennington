using System.Collections.Generic;
using System.Linq;
using Bennington.AdminAccounts.Controllers;
using Simple.Data;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountRepository : IAdminAccountRepository
    {
        public IEnumerable<AdminAccount> GetAllAdminAccounts()
        {
            var db = Database.OpenConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=test;Trusted_Connection=True;");
            var all = db.AdminAccounts.All();
            var castAs = (IEnumerable<AdminAccount>)all.Cast<AdminAccount>();
            return castAs.ToList();
        }
    }
}