using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Routing
{
    public class SectionNodeProviderRouteRegistrator : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.MapRoute("ContentTreeSectionNode", "ContentTreeSectionNode/{action}", new { controller = "ContentTreeSectionNode", action = "Index", id = UrlParameter.Optional  }, null);
        }
    }
}
