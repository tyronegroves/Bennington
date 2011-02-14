using System.Web.Mvc;
using Bennington.Cms.Models;
using Bennington.Core.MenuSystem;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.Controllers
{
    public class MenuSystemController : Controller
    {
		private readonly IServiceLocator serviceLocator;

		public MenuSystemController(IServiceLocator serviceLocator)
		{
			this.serviceLocator = serviceLocator;
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

    }
}
