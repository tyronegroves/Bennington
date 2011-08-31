using System;
using System.Web.Mvc;
using Bennington.Cms.Models;
using Bennington.Core.MenuSystem;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.Controllers
{
    public class MenuSystemController : Controller
    {
        private readonly ISectionMenuRetriever sectionMenuRetriever;
        private readonly ISubMenuRetriever subMenuRetriever;
        private readonly IServiceLocator serviceLocator;

        public MenuSystemController(ISectionMenuRetriever sectionMenuRetriever,
            ISubMenuRetriever subMenuRetriever, IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            this.sectionMenuRetriever = sectionMenuRetriever;
            this.subMenuRetriever = subMenuRetriever;
        }

        public ActionResult GetSectionMenuViewModel()
        {
            var menuItems = serviceLocator.ResolveServices<IAmASectionMenuItem>();
            return View("GetSectionMenuViewModel", new SectionMenuViewModel()
                                                    {
                                                        MenuItems = menuItems,
                                                    });
        }

        public ActionResult GetIconMenuViewModel()
        {
            var menuItems = serviceLocator.ResolveServices<IAmAnIconMenuItem>();
            return View("GetIconMenuViewModel", new IconMenuViewModel
                                                    {
                                                        IconMenuItems = menuItems,
                                                    });
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