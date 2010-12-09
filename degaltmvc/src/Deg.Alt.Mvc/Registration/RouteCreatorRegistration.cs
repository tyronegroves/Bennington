using Deg.Alt.Mvc.Mappers;
using Deg.Alt.Mvc.Routing;
using Deg.Alt.Mvc.Routing.RouteCreators;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Registration
{
    public class RouteCreatorRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator serviceLocator)
        {
            serviceLocator.Register<IRouteCreator, ContentPageInSectionRouteCreator>("ContentPageInSectionRouteCreator");
            serviceLocator.Register<IRouteCreator, PageInRootRouteCreator>("PageInRootRouteCreator");
            serviceLocator.Register<IRouteCreator, RootPageRouteCreator>("RootPageRouteCreator");
            serviceLocator.Register<IRouteCreator, SectionPageRouteCreator>("SectionPageRouteCreator");
        }
    }
}