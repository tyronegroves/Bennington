using System;
using System.Collections.Generic;
using System.Linq;
using Bennington.AdminAccounts.Controllers;
using Bennington.AdminAccounts.Data;
using Bennington.AdminAccounts.Passwords;
using Simple.Data;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountRepository : IAdminAccountRepository
    {
        private readonly IDatabaseRetriever databaseRetriever;
        private readonly IPasswordHasher passwordHasher;

        public AdminAccountRepository(IDatabaseRetriever databaseRetriever,
            IPasswordHasher passwordHasher)
        {
            this.databaseRetriever = databaseRetriever;
            this.passwordHasher = passwordHasher;
        }

        public IEnumerable<AdminAccount> GetAllAdminAccounts()
        {
            var all = databaseRetriever.GetTheDatabase().AdminAccounts.All();
            var castAs = (IEnumerable<AdminAccount>)all.Cast<AdminAccount>();
            return castAs.ToList();
        }

        public AdminAccount GetAdminAccountByUsernameAndPassword(string username, string password)
        {
            AdminAccount adminAccount = databaseRetriever.GetTheDatabase()
                .AdminAccounts.FindByUsernameAndPassword(username, passwordHasher.GetHash(password));
            return adminAccount;
        }
    }
}