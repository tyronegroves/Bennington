using System.Web.Routing;

namespace Paragon.ContentTree.Routing.Routing
{
    public interface IWantCustomRouting
    {
        RouteBase GetCustomRoute();
        void RemoveRoute(RouteCollection routes);
    }
}