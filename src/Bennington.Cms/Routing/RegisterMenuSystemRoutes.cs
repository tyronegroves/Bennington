using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.Cms.Routing
{
	public class RegisterMenuSystemRoutes : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"system.menu/{action}",
				new { controller = "MenuSystem", action = "Index" }
				);
		}
	}
}