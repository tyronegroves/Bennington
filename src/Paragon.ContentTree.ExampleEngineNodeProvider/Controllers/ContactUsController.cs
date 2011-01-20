using System.Web.Mvc;

namespace Paragon.ContentTree.ExampleEngineNodeProvider.Controllers
{
    public class ContactUsController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}

        public ActionResult Confirmation()
        {
            return View();
        }

		public ActionResult Confirmation2()
		{
			return View();
		}
	}
}
