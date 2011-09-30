using System;
using System.Web.Mvc;
using Bennington.Cms.Models;

namespace Bennington.Cms.MenuSystem
{
    public class UrlSubMenuItem : ISubMenuItem
    {
        private readonly string name;
        private readonly string url;
        private readonly Func<ControllerContext, bool> visibleFunction;

        public UrlSubMenuItem(string name, string url)
            : this(name, url, null)
        {
        }

        public UrlSubMenuItem(string name, string url, Func<ControllerContext, bool> visibleFunction)
        {
            this.name = name;
            this.url = url;
            this.visibleFunction = visibleFunction;
        }

        public SubMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            return new SubMenuItemViewModel {Name = name, Url = url, Visible = visibleFunction != null && visibleFunction(controllerContext)};
        }
    }
}