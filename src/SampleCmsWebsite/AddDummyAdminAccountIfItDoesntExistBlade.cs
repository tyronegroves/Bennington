using System;
using System.Linq;
using Bennington.Cms.PrincipalProvider.Encryption;
using Bennington.Cms.PrincipalProvider.Models;
using Bennington.Cms.PrincipalProvider.Repositories;
using MvcTurbine;
using MvcTurbine.Blades;

namespace SampleCmsWebsite
{
    public class AddDummyAdminAccountIfItDoesntExistBlade : Blade
    {
        private readonly IUserRepository userRepository;
        private readonly IEncryptionService encryptionService;

        public AddDummyAdminAccountIfItDoesntExistBlade(IUserRepository userRepository,
                                                        IEncryptionService encryptionService)
        {
            this.encryptionService = encryptionService;
            this.userRepository = userRepository;
        }

        public override void Spin(IRotorContext context)
        {
            if (!userRepository.GetAll().Where(a => a.Username.Equals("admin", StringComparison.InvariantCultureIgnoreCase)).Any())
            {
                userRepository.SaveAndReturnId(new User()
                                                   {
                                                       Id = Guid.NewGuid().ToString(),
                                                       Username = "admin",
                                                       Password = encryptionService.Encrypt("admin"),
                                                   });
            }
        }
    }

    
}