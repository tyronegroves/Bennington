﻿using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.Cms.Routing
{
    public class EmptyDashboardRoute : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.MapRoute(
                null,
                "",
                new {controller = "EmptyDashboard", action = "Index"}
                );
        }
    }
}