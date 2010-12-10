using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Paragon.Cms.Routing
{
	public class RegisterDefaultRoutes : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"Manage/MenuSystemController/{action}",
				new { controller = "MenuSystem", action = "Index" }
				);
		}
	}
}