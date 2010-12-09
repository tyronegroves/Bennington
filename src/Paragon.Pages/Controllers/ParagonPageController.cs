using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Paragon.Pages.Content;

namespace Paragon.Pages.Controllers
{
    public class ParagonPageController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}
    }
}