using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Paragon.ContentTree.Engines.Homepage.Routing
{
	public class HomepageRouteRegistrator : IRouteRegistrator
	{
		private readonly HomepageRouteConstraint homepageRouteConstraint;

		public HomepageRouteRegistrator(HomepageRouteConstraint homepageRouteConstraint)
		{
			this.homepageRouteConstraint = homepageRouteConstraint;
		}

		public void Register(RouteCollection routes)
		{
			var route = new Route
						(
							"",
							new RouteValueDictionary()
			           		           	{
			           		           		{"action", "Index"},
											{"controller", "Homepage"},
			           		           	},
							new MvcRouteHandler()
						);

			route.Constraints = new RouteValueDictionary();
			route.Constraints.Add(typeof(HomepageRouteConstraint).AssemblyQualifiedName, homepageRouteConstraint);
			
			routes.Add(route);
		}
	}
}
