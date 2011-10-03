using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace MvcApplication1.Routing
{
    public class SampeContentSiteControllersRouteRegistrator : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Home", // Route name
                "", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "HomeActions", // Route name
                "Home/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Account", // Route name
                "Account/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}