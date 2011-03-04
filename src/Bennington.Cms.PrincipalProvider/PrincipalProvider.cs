using System.Linq;
using System.Security.Principal;
using Bennington.Cms.PrincipalProvider.Encryption;
using Bennington.Cms.PrincipalProvider.Repositories;
using MvcTurbine.MembershipProvider;

namespace Bennington.Cms.PrincipalProvider
{
    public class PrincipalProvider : IPrincipalProvider
    {
    	private readonly IUserRepository userRepository;
    	private readonly IEncryptionService encryptionService;

    	public PrincipalProvider(IUserRepository userRepository, IEncryptionService encryptionService)
    	{
    		this.encryptionService = encryptionService;
    		this.userRepository = userRepository;
    	}

    	public PrincipalProviderResult GetPrincipal(string userId, string password)
        {
			var user = userRepository.GetAll().Where(a => a.Username == userId).FirstOrDefault();
			if (user != null)
			{
				if (encryptionService.Encrypt(user.Password) == password)
					return new PrincipalProviderResult
					{
						Principal = new GenericPrincipal(new GenericIdentity(user.Username), new string[] { })
					};				
			}

			return new PrincipalProviderResult
						{
							Principal = null
						};
        }

        public IPrincipal CreatePrincipalFromTicketData(string userName, string userData)
        {
            return new GenericPrincipal(new GenericIdentity(userName), new string[] {});
        }

        public TicketData ConvertPrincipalToTicketData(IPrincipal principal)
        {
            return new TicketData
                       {
                           IsPersistent = true,
                           NumberOfMinutesUntilExpiration = 15,
                           Username = principal.Identity.Name
                       };
        }
    }
}