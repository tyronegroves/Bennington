using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.MenuSystem;
using Bennington.Core.List;
using MvcTurbine;
using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.Blades
{
    public class CmsBlade : Blade, ISupportAutoRegistration
    {
        public override void Spin(IRotorContext context)
        {
            ModelBinders.Binders.Add(typeof(ListViewModel), new ListViewModelBinder());

            var serviceLocator = context.ServiceLocator;
            var configurers = serviceLocator.ResolveServices<IMenuSystemConfigurer>().ToList();
            var menuRegistry = GetMenuRegistryOrRegisterDefaultInstance(serviceLocator);

            configurers.ForEach(configurer => configurer.Configure(menuRegistry));
        }

        private static IMenuRegistry GetMenuRegistryOrRegisterDefaultInstance(IServiceLocator serviceLocator)
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
            registrationList.Add(Registration.Simple<IMenuSystemConfigurer>());
        }
    }
}