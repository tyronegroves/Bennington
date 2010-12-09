using Deg.Alt.Mvc.Routing.RouteConstraints;
using Deg.Alt.Mvc.Routing.RouteCreators;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Registration
{
    public class RouteConstraintRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register(typeof (IContentPageInSectionRouteConstraint), typeof (ContentPageInSectionRouteConstraint));
            locator.Register(typeof (IPageInRootRouteConstraint), typeof (PageInRootRouteConstraint));
            locator.Register(typeof (ISectionRouteConstraint), typeof (SectionRouteConstraint));
        }
    }
}