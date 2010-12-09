using System.Web.Routing;
using Deg.Alt.Mvc.Routing.InstinctRouteHandlers;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Routing.RouteCreators
{
    public class RootPageRouteCreator : IRouteCreator
    {
        private readonly IServiceLocator serviceLocator;

        public RootPageRouteCreator(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public Route CreateRoute()
        {
            return new Route(string.Empty,
                             new InstinctRouteHandler(serviceLocator))
                       {
                           Defaults = new RouteValueDictionary(
                               new
                                   {
                                       sectionId = string.Empty,
                                       pageId = "Index_",
                                       controller = "Index_",
                                       action = "Index",
                                       id = string.Empty,
                                   }),
                       };
        }
    }
}