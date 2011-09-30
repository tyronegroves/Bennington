using System.Web.Mvc;
using Bennington.Cms.Models;

namespace Bennington.Cms.MenuSystem
{
    public class UrlSectionMenuItem : ISectionMenuItem
    {
        private readonly string name;
        private readonly string url;

        public UrlSectionMenuItem(string name, string url)
        {
            this.name = name;
            this.url = url;
        }

        public SectionMenuItemViewModel GetViewModel(ControllerContext controllerContext)
        {
            return new SectionMenuItemViewModel {Name = name, Url = url};
        }
    }
}