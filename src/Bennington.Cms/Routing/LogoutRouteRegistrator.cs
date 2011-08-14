using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.Cms.Routing
{
	public class LogoutRouteRegistrator : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
					null,
					"Logout/{action}",
					new { controller = "Logout", action = "Index" }
					);
		}
	}
}