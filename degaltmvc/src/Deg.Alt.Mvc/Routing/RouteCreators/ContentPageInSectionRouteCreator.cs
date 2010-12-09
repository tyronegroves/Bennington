using System.Web.Routing;
using Deg.Alt.Mvc.Routing.InstinctRouteHandlers;
using Deg.Alt.Mvc.Routing.RouteConstraints;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Routing.RouteCreators
{
    public class ContentPageInSectionRouteCreator : IRouteCreator
    {
        private readonly IContentPageInSectionRouteConstraint contentPageInSectionRouteConstraint;
        private readonly IServiceLocator serviceLocator;

        public ContentPageInSectionRouteCreator(
            IContentPageInSectionRouteConstraint contentPageInSectionRouteConstraint,
            IServiceLocator serviceLocator)
        {
            this.contentPageInSectionRouteConstraint = contentPageInSectionRouteConstraint;
            this.serviceLocator = serviceLocator;
        }

        public Route CreateRoute()
        {
            return new Route("{sectionId}/{controller}/{action}/{id}",
                             new InstinctRouteHandler(serviceLocator))
                       {
                           Defaults =
                               new RouteValueDictionary(
                               new
                                   {
                                       sectionId = string.Empty,
                                       pageId = string.Empty,
                                       controller = "Content",
                                       action = "Index",
                                       id = string.Empty,
                                   }),
                           Constraints = new RouteValueDictionary(new {Auth = contentPageInSectionRouteConstraint,})
                       };
        }
    }
}