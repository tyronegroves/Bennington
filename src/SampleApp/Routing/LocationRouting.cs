using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace SampleApp.Routing
{
    public class LocationRouting : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.MapRoute(
                null,
                "Location/{action}/{id}",
                new { controller = "Location", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}