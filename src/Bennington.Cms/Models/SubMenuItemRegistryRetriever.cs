using System.Collections.Generic;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.Models
{
    public class SubMenuItemRegistryRetriever : ISubMenuItemRegistryRetriever
    {
        private readonly IServiceLocator serviceLocator;

        public SubMenuItemRegistryRetriever(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public IEnumerable<ISubMenuItemRegistry> GetTheRegistries()
        {
            return serviceLocator.ResolveServices<ISubMenuItemRegistry>();
        }
    }
}