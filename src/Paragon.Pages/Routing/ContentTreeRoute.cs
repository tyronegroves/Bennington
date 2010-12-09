using System.Web.Mvc;
using System.Web.Routing;

namespace Paragon.ContentTree.Routing.Routing
{
    public class ContentTreeRoute : Route
    {
        public ContentTreeRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints) 
            : base(url, defaults, constraints, new MvcRouteHandler())
        {
        }
    }
}