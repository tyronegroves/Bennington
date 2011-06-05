using Bennington.Cms.Filters;

namespace Bennington.Cms.Models
{
    public interface ICurrentSituation
    {
        string Controller { get; }
        string Action { get; }
    }

    public class CurrentSituation : ICurrentSituation
    {
        private readonly IRouteDataRetriever routeDataRetriever;

        public CurrentSituation(IRouteDataRetriever routeDataRetriever)
        {
            this.routeDataRetriever = routeDataRetriever;
        }

        public string Controller
        {
            get
            {
                return routeDataRetriever.GetRouteData().Values["callingController"] as string
                    ?? routeDataRetriever.GetRouteData().Values["controller"] as string;
            }
        }

        public string Action
        {
            get
            {
                return routeDataRetriever.GetRouteData().Values["callingAction"] as string 
                    ?? routeDataRetriever.GetRouteData().Values["action"] as string;
            }
        }
    }
}