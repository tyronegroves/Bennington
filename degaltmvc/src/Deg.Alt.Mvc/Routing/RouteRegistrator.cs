using System.Web.Routing;
using MvcTurbine.Routing;

namespace Deg.Alt.Mvc.Routing
{
    public class RouteRegistrator : IRouteRegistrator
    {
        private readonly IRouteCreator[] routeCreators;

        public RouteRegistrator(IRouteCreator[] routeCreators)
        {
            this.routeCreators = routeCreators;
        }

        public void Register(RouteCollection routes)
        {
            foreach (var routeCreator in routeCreators)
                routes.Add(routeCreator.CreateRoute());
        }
    }
}