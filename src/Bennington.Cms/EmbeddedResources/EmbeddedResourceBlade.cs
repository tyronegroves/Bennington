using System.Web.Hosting;
using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.EmbeddedResources
{
    public class EmbeddedResourceBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            var locator = GetServiceLocatorFromContext(context);
            var resolver = GetEmbeddedResourceResolver(locator);
            var table = resolver.GetEmbeddedResources();
            var provider = new EmbeddedResourceVirtualPathProvider(table);
            HostingEnvironment.RegisterVirtualPathProvider(provider);
        }

        protected virtual IEmbeddedResourceResolver GetEmbeddedResourceResolver(IServiceLocator locator)
        {
            try
            {
                return locator.Resolve<IEmbeddedResourceResolver>();
            }
            catch
            {
                return new EmbeddedResourceResolver();
            }
        }
    }
}