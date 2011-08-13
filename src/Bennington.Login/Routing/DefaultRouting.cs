using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.Login.Routing
{
    public class DefaultRouting : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}