using System;
using System.Web.Mvc;
using Bennington.Cms.Models;

namespace Bennington.Cms.Controllers
{
    public class MenuSystemController : Controller
    {
        private readonly ISectionMenuRetriever sectionMenuRetriever;
        private readonly ISubMenuRetriever subMenuRetriever;

        public MenuSystemController(ISectionMenuRetriever sectionMenuRetriever,
            ISubMenuRetriever subMenuRetriever)
        {
            this.sectionMenuRetriever = sectionMenuRetriever;
            this.subMenuRetriever = subMenuRetriever;
        }

        public ActionResult GetSectionMenuViewModel()
        {
            //var menuItems = serviceLocator.ResolveServices<IAmASectionMenuItem>();
            //return View("GetSectionMenuViewModel", new SectionMenuViewModel()
            //                                        {
            //                                            MenuItems = menuItems,
            //                                        });
            return null;
        }

        public ActionResult GetIconMenuViewModel()
        {
            //var menuItems = serviceLocator.ResolveServices<IAmAnIconMenuItem>();
            //return View("GetIconMenuViewModel", new IconMenuViewModel
            //                                        {
            //                                            IconMenuItems = menuItems,
            //                                        });
            return null;
        }

        public ActionResult SectionMenu()
        {
            return View("SectionMenu", sectionMenuRetriever.GetTheSectionMenu());
        }

        public ActionResult SubMenu()
        {
            return View("SubMenu", subMenuRetriever.GetTheSubMenu());
        }
    }
}