using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace DEGSMS4.Routing
{
    public class RegisterDefaultRoutes : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new {controller = "Home", action = "Index"}
                );
        }
    }
}