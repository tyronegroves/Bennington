using Deg.Alt.Mvc.Helpers;
using Deg.Alt.Mvc.Routing;
using MvcTurbine;
using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Blades
{
    public class DefaultRegistrationBlade : Blade
    {
        public DefaultRegistrationBlade(IServiceLocator serviceLocator)
        {
            // hate to put this logic in the constructor, but it's the earliest
            // place I can put this registration and allow it to be overridden
            // with an instance of IServiceRegistration
            RegisterControllerExcludedRegistryIfItHasNotBeenRegisteredAlready(serviceLocator);
        }

        public override void Spin(IRotorContext context)
        {
        }

        private static void RegisterControllerExcludedRegistryIfItHasNotBeenRegisteredAlready(IServiceLocator serviceLocator)
        {
            try
            {
                serviceLocator.Resolve<IControllersExcludedFromRoutingRegistry>();
            }
            catch
            {
                serviceLocator.Register<IControllersExcludedFromRoutingRegistry, EmptyControllersExcludedFromRoutingRegistry>();
            }
        }
    }
}