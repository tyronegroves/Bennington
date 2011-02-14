using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.ContentTree.TreeManager.Routing
{
	public class RegisterContentTreeSmsRoutes : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"Manage/ContentTree/{action}",
				new { controller = "TreeManager", action = "Index" }
				);

			routes.MapRoute(
				null,
				"Manage",
				new { controller = "TreeManager", action = "Index" }
				);
		}
	}
}