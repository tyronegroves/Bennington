using System.Web.Routing;

namespace Paragon.Pages.Routing
{
    public interface IWantCustomRouting
    {
        RouteBase GetCustomRoute();
        void RemoveRoute(RouteCollection routes);
    }
}