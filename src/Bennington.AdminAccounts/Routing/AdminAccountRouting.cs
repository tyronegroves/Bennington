using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace SampleApp.Routing
{
    public class AdminAccountRouting : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.MapRoute(
                null,
                "AdminAccount/{action}/{id}",
                new { controller = "AdminAccount", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}