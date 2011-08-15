using Bennington.AdminAccounts.Controllers;
using Bennington.AdminAccounts.Data;
using Bennington.AdminAccounts.Helpers;
using Bennington.AdminAccounts.Models;
using Bennington.AdminAccounts.Passwords;
using MvcTurbine.ComponentModel;

namespace Bennington.AdminAccounts.Registration
{
    public class AdminAccountRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register<IAdminAccountRepository, AdminAccountRepository>();
            locator.Register<IAdminAccountListPageViewModelMapper, AdminAccountListPageViewModelMapper>();
            locator.Register<IAdminAccountEditFormStore, AdminAccountEditFormStore>();
            locator.Register<IAdminAccountIdGenerator, AdminAccountIdGenerator>();

            locator.Register<IDatabaseRetriever>(() =>
                                                     {
                                                         var adminAccountSettings = locator.Resolve<IAdminAccountSettings>();
                                                         return new DatabaseRetriever(adminAccountSettings.ConnectionString);
                                                     });

            locator.Register<IPasswordHasher>(() =>
                                                  {
                                                      var adminAccountSettings = locator.Resolve<IAdminAccountSettings>();
                                                      return new PasswordHasher(adminAccountSettings);
                                                  });
        }
    }
}