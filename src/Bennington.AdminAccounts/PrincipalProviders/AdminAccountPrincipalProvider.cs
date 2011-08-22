using System.Security.Principal;
using Bennington.AdminAccounts.Models;
using MvcTurbine.MembershipProvider;

namespace Bennington.AdminAccounts.PrincipalProviders
{
    public class AdminAccountPrincipalProvider : PrincipalProvider
    {
        private readonly IAdminAccountRepository adminAccountRepository;

        public AdminAccountPrincipalProvider(IAdminAccountRepository adminAccountRepository)
        {
            this.adminAccountRepository = adminAccountRepository;
        }

        public override PrincipalProviderResult GetPrincipal(string userId, string password)
        {
            var adminAccount = adminAccountRepository.GetAdminAccountByUsernameAndPassword(userId, password);
            if (adminAccount == null) return new PrincipalProviderResult();
            return new PrincipalProviderResult
                       {
                           Principal = new GenericPrincipal(new GenericIdentity(adminAccount.FirstName + " " + adminAccount.LastName), new string[] {})
                       };
        }
    }
}