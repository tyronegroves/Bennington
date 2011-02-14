using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.Cms.Routing
{
	public class AccountRouteRegistrator : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
					null,
					"Manage/Account/{action}",
					new { controller = "Account", action = "Index" }
					);
		}
	}
}