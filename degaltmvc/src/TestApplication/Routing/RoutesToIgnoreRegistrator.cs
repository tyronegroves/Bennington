using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace TestApplication.Routing
{
    public class RoutesToIgnoreRegistrator : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
        }
    }
}