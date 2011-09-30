using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Cms.Models;

namespace Bennington.Cms.MenuSystem
{
    public class ActionSubMenuItem : ISubMenuItem
    {
        private readonly string name;
        private readonly string actionName;
        private readonly string controllerName;
        private readonly RouteValueDictionary routeValues;

        public ActionSubMenuItem(string name, string actionName, string controllerName)
            : this(name, actionName, controllerName, null)
        {
        }

        public ActionSubMenuItem(string name, string actionName, string controllerName, object routeValues)
        {
            this.name = name;
            this.actionName = actionName;
            this.controllerName = controllerName;
            this.routeValues = new RouteValueDictionary(routeValues);
        }

        public SubMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            var urlHelper = new UrlHelper(controllerContext.RequestContext);
            var routeData = GetRootRouteData(controllerContext);
            return new SubMenuItemViewModel
                       {
                           Name = name, Url = urlHelper.Action(actionName, controllerName, routeValues), 
                           Selected = routeData.GetRequiredString("controller") == controllerName && routeData.GetRequiredString("action") == actionName, 
                           Visible = routeData.GetRequiredString("controller") == controllerName
                       };
        }

        private static RouteData GetRootRouteData(ControllerContext controllerContext)
        {
            return controllerContext.IsChildAction ? GetRootRouteData(controllerContext.ParentActionViewContext) : controllerContext.RouteData;
        }
    }
}