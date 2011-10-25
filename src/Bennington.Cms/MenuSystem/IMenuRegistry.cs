using System.Web.Mvc;
using Bennington.Cms.Models.MenuSystem;

namespace Bennington.Cms.MenuSystem
{
    public interface IMenuRegistry
    {
        void Add(IIconMenuItem iconMenuItem);
        void Add(ISubMenuItem subMenuItem);
        void Add(ISectionMenuItem sectionMenuItem);
        SectionMenuViewModel GetSectionMenu(ControllerContext controllerContext);
        SubMenuViewModel GetSubMenu(ControllerContext controllerContext);
        IconMenuViewModel GetIconMenu(ControllerContext controllerContext);
    }
}