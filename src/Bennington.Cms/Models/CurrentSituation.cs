using System.Web.Routing;
using Bennington.Cms.Filters;

namespace Bennington.Cms.Models
{
    public interface ICurrentSituation
    {
        string Controller { get; }
        string Action { get; }
        string Id { get; }
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
                return GetTheRouteData()["callingController"] as string
                       ?? GetTheRouteData()["controller"] as string;
            }
        }

        private RouteValueDictionary GetTheRouteData()
        {
            var routeData = routeDataRetriever.GetRouteData();
            return routeData != null ? routeData.Values : new RouteValueDictionary();
        }

        public string Action
        {
            get
            {
                return GetTheRouteData()["callingAction"] as string
                       ?? GetTheRouteData()["action"] as string;
            }
        }

        public string Id
        {
            get
            {
                return GetTheRouteData()["callingId"] as string
                       ?? GetTheRouteData()["id"] as string;
            }
        }
    }
}