using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Paragon.ContentTree.TreeManager.Routing
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
		}
	}
}