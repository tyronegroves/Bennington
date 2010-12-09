using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Paragon.Cms.Routing
{
    public class RegisterDefaultRoutes : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
			//routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"ManageDefault",
				"Manage/{controller}/{action}",
				new { controller = "Home", action = "Index" }
				);
        }
    }
}