using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Cms.Models;

namespace Bennington.Cms.MenuSystem
{
    public class ActionIconMenuItem : IIconMenuItem
    {
        private readonly string name;
        private readonly string iconUrl;
        private readonly string actionName;
        private readonly string controllerName;
        private readonly RouteValueDictionary routeValues;

        public ActionIconMenuItem(string name, string iconUrl, string actionName, string controllerName)
            : this(name, iconUrl, actionName, controllerName, null)
        {
        }

        public ActionIconMenuItem(string name, string iconUrl, string actionName, string controllerName, object routeValues)
        {
            this.name = name;
            this.iconUrl = iconUrl;
            this.actionName = actionName;
            this.controllerName = controllerName;
            this.routeValues = new RouteValueDictionary(routeValues);
        }

        public IconMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            var urlHelper = new UrlHelper(controllerContext.RequestContext);
            return new IconMenuItemViewModel{Name = name, Url = urlHelper.Action(actionName, controllerName, routeValues), IconUrl = iconUrl };
        }
    }
}