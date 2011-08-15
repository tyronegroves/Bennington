using System;
using Bennington.AdminAccounts.Data;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountEditFormStore : IAdminAccountEditFormStore
    {
        private readonly IDatabaseRetriever databaseRetriever;

        public AdminAccountEditFormStore(IDatabaseRetriever databaseRetriever)
        {
            this.databaseRetriever = databaseRetriever;
        }

        public AdminAccountEditForm GetForm(string id)
        {
            var adminAccount = databaseRetriever.GetTheDatabase().AdminAccounts
                .FindById(new Guid(id));
            return new AdminAccountEditForm
                       {
                           FirstName = adminAccount.FirstName,
                           LastName = adminAccount.LastName,
                           Id = adminAccount.Id.ToString().ToUpper(),
                           Username = adminAccount.Username
                       };
        }
    }
}