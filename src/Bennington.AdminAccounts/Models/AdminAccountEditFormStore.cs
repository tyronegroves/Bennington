using System;
using Bennington.AdminAccounts.Data;
using Bennington.AdminAccounts.Passwords;

namespace Bennington.AdminAccounts.Models
{
    public class AdminAccountEditFormStore : IAdminAccountEditFormStore
    {
        private readonly IDatabaseRetriever databaseRetriever;
        private readonly IPasswordHasher passwordHasher;

        public AdminAccountEditFormStore(IDatabaseRetriever databaseRetriever, IPasswordHasher passwordHasher)
        {
            this.databaseRetriever = databaseRetriever;
            this.passwordHasher = passwordHasher;
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

        public AdminAccountSaveResult SaveForm(AdminAccountEditForm adminAccountEditForm)
        {
            var database = databaseRetriever.GetTheDatabase();

            if (database.AdminAccounts.FindAllById(new Guid(adminAccountEditForm.Id)).Any())
                database
                    .AdminAccounts
                    .UpdateById(Id: new Guid(adminAccountEditForm.Id),
                                FirstName: adminAccountEditForm.FirstName,
                                LastName: adminAccountEditForm.LastName,
                                Username: adminAccountEditForm.Username,
                                Password: passwordHasher.GetHash(adminAccountEditForm.Password));

            else
                database.AdminAccounts
                    .Insert(Id: new Guid(adminAccountEditForm.Id),
                            FirstName: adminAccountEditForm.FirstName,
                            LastName: adminAccountEditForm.LastName,
                            Username: adminAccountEditForm.Username,
                            Password: passwordHasher.GetHash(adminAccountEditForm.Password));
            return null;
        }
    }
}