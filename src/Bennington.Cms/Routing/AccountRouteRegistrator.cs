using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Paragon.Cms.Routing
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