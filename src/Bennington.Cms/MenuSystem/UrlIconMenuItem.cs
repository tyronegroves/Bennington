using System.Web.Mvc;
using Bennington.Cms.Models.MenuSystem;

namespace Bennington.Cms.MenuSystem
{
    public class UrlIconMenuItem : IIconMenuItem
    {
        private readonly string name;
        private readonly string url;
        private readonly string iconUrl;

        public UrlIconMenuItem(string name, string url, string iconUrl)
        {
            this.name = name;
            this.url = url;
            this.iconUrl = iconUrl;
        }

        public IconMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            return new IconMenuItemViewModel {Name = name, Url = url, IconUrl = iconUrl};
        }
    }
}