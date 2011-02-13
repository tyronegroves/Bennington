using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTurbine.ComponentModel;
using Paragon.Cms.Models;
using Paragon.Core.MenuSystem;

namespace Paragon.Cms.Controllers
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
