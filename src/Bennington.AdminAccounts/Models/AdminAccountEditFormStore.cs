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
                           Id = adminAccount.Id.ToString(),
                           Username = adminAccount.Username
                       };
        }

        public void SaveForm(AdminAccountEditForm adminAccountEditForm)
        {
            if (databaseRetriever.GetTheDatabase().AdminAccounts.FindAllById(new Guid(adminAccountEditForm.Id)).Any())
                databaseRetriever.GetTheDatabase()
                    .AdminAccounts
                    .UpdateById(Id: new Guid(adminAccountEditForm.Id),
                                FirstName: adminAccountEditForm.FirstName,
                                LastName: adminAccountEditForm.LastName,
                                Username: adminAccountEditForm.Username);

            else
                databaseRetriever.GetTheDatabase().AdminAccounts
                    .Insert(Id: new Guid(adminAccountEditForm.Id),
                            FirstName: adminAccountEditForm.FirstName,
                            LastName: adminAccountEditForm.LastName,
                            Username: adminAccountEditForm.Username);

            if (string.IsNullOrEmpty(adminAccountEditForm.Password) == false)
                databaseRetriever.GetTheDatabase()
                    .AdminAccounts
                    .UpdateById(Id: new Guid(adminAccountEditForm.Id),
                                Password: passwordHasher.GetHash(adminAccountEditForm.Password));
        }
    }
}