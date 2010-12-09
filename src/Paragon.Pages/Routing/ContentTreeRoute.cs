using System.Web.Mvc;
using System.Web.Routing;

namespace Paragon.Pages.Routing
{
    public class ContentTreeRoute : Route
    {
        public ContentTreeRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints) 
            : base(url, defaults, constraints, new MvcRouteHandler())
        {
        }
    }
}