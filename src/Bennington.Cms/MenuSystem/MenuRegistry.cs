using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.Models;

namespace Bennington.Cms.MenuSystem
{
    public class MenuRegistry : IMenuRegistry
    {
        private readonly List<IIconMenuItem> iconMenuItems = new List<IIconMenuItem>();
        private readonly List<ISubMenuItem> subMenuItems = new List<ISubMenuItem>();
        private readonly List<ISectionMenuItem> sectionMenuItems = new List<ISectionMenuItem>();

        public SectionMenuViewModel GetSectionMenu(ControllerContext controllerContext)
        {
            return new SectionMenuViewModel {Items = sectionMenuItems.Select(s => s.GetViewModel(controllerContext)).ToList()};
        }

        public SubMenuViewModel GetSubMenu(ControllerContext controllerContext)
        {
            return new SubMenuViewModel {Items = subMenuItems.Select(s => s.GetViewModel(controllerContext)).Where(s => s.Visible).ToList()};
        }

        public IconMenuViewModel GetIconMenu(ControllerContext controllerContext)
        {
            return new IconMenuViewModel { Items = iconMenuItems.Select(i => i.GetViewModel(controllerContext)).ToList() };
        }

        public void Add(IIconMenuItem iconMenuItem)
        {
            iconMenuItems.Add(iconMenuItem);
        }

        public void Add(ISubMenuItem subMenuItem)
        {
            subMenuItems.Add(subMenuItem);
        }

        public void Add(ISectionMenuItem sectionMenuItem)
        {
            sectionMenuItems.Add(sectionMenuItem);
        }
    }
}