using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;

namespace Paragon.ContentTree.ContentNodeProvider.Routing
{
    public class RegisterDefaultRoutes : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
			routes.MapRoute(
				"ContentTreeNodeDisplayRoute",
				"DisplayContentTreeNode____",
				new { controller = "ContentTreeNode", action = "Index" }
				);
        }
    }
}