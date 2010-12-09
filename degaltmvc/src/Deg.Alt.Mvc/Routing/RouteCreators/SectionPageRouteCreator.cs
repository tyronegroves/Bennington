using System.Web.Routing;
using Deg.Alt.Mvc.Routing.InstinctRouteHandlers;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Routing.RouteCreators
{
    public class SectionPageRouteCreator : IRouteCreator
    {
        private readonly ISectionRouteConstraint sectionRouteConstraint;
        private readonly IServiceLocator serviceLocator;

        public SectionPageRouteCreator(ISectionRouteConstraint sectionRouteConstraint,
                                       IServiceLocator serviceLocator)
        {
            this.sectionRouteConstraint = sectionRouteConstraint;
            this.serviceLocator = serviceLocator;
        }

        public Route CreateRoute()
        {
            return new Route("{sectionId}/{action}/{id}",
                             new InstinctSectionRouteHandler(serviceLocator))
                       {
                           Defaults =
                               new RouteValueDictionary(
                               new
                                   {
                                       sectionId = "",
                                       pageId = "",
                                       controller = "Controller",
                                       action = "Index",
                                       id = "",
                                   }),
                           Constraints = new RouteValueDictionary(new {Auth = sectionRouteConstraint,}),
                       };
        }
    }
}