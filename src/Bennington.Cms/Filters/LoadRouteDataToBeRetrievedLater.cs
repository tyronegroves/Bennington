using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bennington.Cms.Filters
{
    public class RegisterTheDefaultMasterPageSetting : MvcTurbine.Web.Filters.GlobalFilterRegistry
    {
        public RegisterTheDefaultMasterPageSetting()
        {
            AsGlobal<LoadRouteData>();
        }
    }

    public class LoadRouteData : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routeData = filterContext.RouteData;
            HttpContext.Current.Items["RouteDataForLater"] = routeData;
            base.OnActionExecuting(filterContext);
        }
    }

    public interface IRouteDataRetriever
    {
        RouteData GetRouteData();
    }

    public class RouteDataRetriever : IRouteDataRetriever
    {
        public RouteData GetRouteData()
        {
            return HttpContext.Current.Items["RouteDataForLater"] as RouteData;
        }
    }

}