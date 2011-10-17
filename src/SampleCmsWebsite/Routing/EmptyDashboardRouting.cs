using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace SampleCmsWebsite.Routing
{
    public class EmptyDashboardRouting : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.MapRoute("EmptyDashboard", "", new { controller = "EmptyDashboard", action = "Index" });
        }
    }
}