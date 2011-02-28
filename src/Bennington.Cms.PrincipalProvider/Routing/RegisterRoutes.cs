using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.Cms.PrincipalProvider.Routing
{
	public class RegisterRoutes : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"Manage/Users/{action}",
				new { controller = "User", action = "Index" }
				);
		}
	}
}