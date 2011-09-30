using System.Web.Mvc;
using Bennington.Cms.MenuSystem;

namespace Bennington.Cms.Controllers
{
    public class MenuSystemController : Controller
    {
        private readonly IMenuRegistry menuRegistry;

        public MenuSystemController(IMenuRegistry menuRegistry)
        {
            this.menuRegistry = menuRegistry;
        }

        public ActionResult IconMenu()
        {
            return View("IconMenu", menuRegistry.GetIconMenu(ControllerContext));
        }

        public ActionResult SectionMenu()
        {
            return View("SectionMenu", menuRegistry.GetSectionMenu(ControllerContext));
        }

        public ActionResult SubMenu()
        {
            return View("SubMenu", menuRegistry.GetSubMenu(ControllerContext));
        }
    }
}