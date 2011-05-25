using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace SampleApp.Controllers
{
    public class LocationRouting : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.MapRoute(
                    null,
                    "Manage/Location/{action}",
                    new { controller = "Location", action = "Index" }
                    );
        }
    }

    public class LocationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
