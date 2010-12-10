using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Paragon.ContentTree.Routing
{
	public class RegisterDefaultRoutes : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"Manage/ContentTree/{action}",
				new { controller = "ContentTree", action = "Index" }
				);
		}
	}
}