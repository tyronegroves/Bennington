using System.Security.Principal;
using MvcTurbine.MembershipProvider;

namespace SampleApp.Security
{
    public class DefaultSecurity : PrincipalProvider
    {
        public override PrincipalProviderResult GetPrincipal(string userId, string password)
        {
            if (userId == null || userId.ToLower() != "test") return new PrincipalProviderResult();
            return new PrincipalProviderResult
                       {
                           Principal = new GenericPrincipal(new GenericIdentity(userId), new string[] {}),
                       };
        }
    }
}