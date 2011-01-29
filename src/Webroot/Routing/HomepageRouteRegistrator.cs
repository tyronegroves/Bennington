using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Webroot.Routing
{
	public class HomepageRouteRegistrator : IRouteRegistrator
	{
		public void Register(RouteCollection routes)
		{
			routes.MapRoute(
				null,
				"",
				new { controller = "Home", action = "Index" }
			);
		}
	}
}