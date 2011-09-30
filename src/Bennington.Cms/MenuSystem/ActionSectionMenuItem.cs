using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Cms.Models;

namespace Bennington.Cms.MenuSystem
{
    public class ActionSectionMenuItem : ISectionMenuItem
    {
        private readonly string name;
        private readonly string actionName;
        private readonly string controllerName;
        private readonly RouteValueDictionary routeValues;

        public ActionSectionMenuItem(string name, string actionName, string controllerName)
            : this(name, actionName, controllerName, null)
        {
        }

        public ActionSectionMenuItem(string name, string actionName, string controllerName, object routeValues)
        {
            this.name = name;
            this.actionName = actionName;
            this.controllerName = controllerName;
            this.routeValues = new RouteValueDictionary(routeValues);
        }

        public string GetName(ControllerContext controllerContext)
        {
            return name;
        }

        public string GetUrl(ControllerContext controllerContext)
        {
            var urlHelper = new UrlHelper(controllerContext.RequestContext);
            return urlHelper.Action(actionName, controllerName, routeValues);
        }

        public bool IsSelected(ControllerContext controllerContext)
        {
            var routeData = GetRootRouteData(controllerContext);
            return routeData.GetRequiredString("controller") == controllerName;
        }

        public SectionMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            return new SectionMenuItemViewModel {Name = name, Url = GetUrl(controllerContext), Selected = IsSelected(controllerContext)};
        }

        private static RouteData GetRootRouteData(ControllerContext controllerContext)
        {
            return controllerContext.IsChildAction ? GetRootRouteData(controllerContext.ParentActionViewContext) : controllerContext.RouteData;
        }
    }
}