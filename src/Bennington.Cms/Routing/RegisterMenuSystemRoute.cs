using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.Cms.Routing
{
    public class RegisterMenuSystemRoute : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.MapRoute(
                "system.menu",
                "system.menu/{action}",
                new {controller = "MenuSystem", action = "Index"}
                );
        }
    }
}