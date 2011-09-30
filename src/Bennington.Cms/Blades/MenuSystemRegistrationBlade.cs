using System.Linq;
using Bennington.Cms.MenuSystem;
using MvcTurbine;
using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.Blades
{
    public class MenuSystemRegistrationBlade : Blade, ISupportAutoRegistration
    {
        public override void Spin(IRotorContext context)
        {
            var serviceLocator = context.ServiceLocator;
            var configurers = serviceLocator.ResolveServices<IMenuSystemConfigurer>().ToList();
            var menuRegistry = GetMenuRegistryOrRegisterADefaultInstance(serviceLocator);

            configurers.ForEach(configurer => configurer.Configure(menuRegistry));
        }

        private static IMenuRegistry GetMenuRegistryOrRegisterADefaultInstance(IServiceLocator serviceLocator)
        {
            try
            {
                return serviceLocator.Resolve<IMenuRegistry>();
            }
            catch(ServiceResolutionException)
            {
                serviceLocator.Register<IMenuRegistry>(new MenuRegistry());
            }

            return serviceLocator.Resolve<IMenuRegistry>();
        }

        public void AddRegistrations(AutoRegistrationList registrationList)
        {
            registrationList.Add(MvcTurbine.ComponentModel.Registration.Simple<IMenuSystemConfigurer>());
        }
    }
}