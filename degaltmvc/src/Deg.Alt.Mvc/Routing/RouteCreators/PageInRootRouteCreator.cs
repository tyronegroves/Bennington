using System.Web.Routing;
using Deg.Alt.Mvc.Routing.InstinctRouteHandlers;
using Deg.Alt.Mvc.Routing.RouteConstraints;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Routing.RouteCreators
{
    public class PageInRootRouteCreator : IRouteCreator
    {
        private readonly IPageInRootRouteConstraint pageInRootRouteConstraint;
        private readonly IServiceLocator serviceLocator;

        public PageInRootRouteCreator(
            IPageInRootRouteConstraint pageInRootRouteConstraint,
            IServiceLocator serviceLocator)
        {
            this.pageInRootRouteConstraint = pageInRootRouteConstraint;
            this.serviceLocator = serviceLocator;
        }

        public Route CreateRoute()
        {
            return new Route("{controller}/{action}/{id}",
                             new InstinctRouteHandler(serviceLocator))
                       {
                           Defaults =
                               new RouteValueDictionary(
                               new
                                   {
                                       sectionId = string.Empty,
                                       pageId = string.Empty,
                                       controller = "Controller",
                                       action = "Index",
                                       id = string.Empty,
                                   }),
                           Constraints = new RouteValueDictionary(new {Auth = pageInRootRouteConstraint,}),
                       };
        }
    }
}