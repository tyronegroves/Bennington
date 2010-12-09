using System;
using Deg.Alt.ContentProvider;
using MvcTurbine;
using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Blades
{
    public class InstinctContentBlade : Blade, ISupportAutoRegistration
    {

        private readonly IServiceLocator serviceLocator;

        public InstinctContentBlade(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        private static void RegisterAllMappingsInTheServiceLocator(IServiceLocator serviceLocator, IMappingsRegistry mappingsRegistry)
        {
            foreach (var mapping in mappingsRegistry.GetMappings())
                serviceLocator.Register(mapping.From, mapping.To);
        }

        private IMappingsRegistry GetTheMappingsRegistry()
        {
            try
            {
                return serviceLocator.Resolve<IMappingsRegistry>();
            }
            catch (ServiceResolutionException exception)
            {
                serviceLocator.Register(typeof(IMappingsRegistry), typeof(DefaultMappingsRegistry));
                return serviceLocator.Resolve<IMappingsRegistry>();
            }
        }

        public void AddRegistrations(AutoRegistrationList registrationList)
        {

            var mappingsRegistry = GetTheMappingsRegistry();

            RegisterAllMappingsInTheServiceLocator(serviceLocator, mappingsRegistry);
        }

        public override void Spin(IRotorContext context)
        {
            // does nothing
        }
    }
}