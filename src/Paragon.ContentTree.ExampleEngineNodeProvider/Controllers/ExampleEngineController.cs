using System.Web.Mvc;

namespace Paragon.ContentTree.ExampleEngineNodeProvider.Controllers
{
    public class ExampleEngineController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}

        public ActionResult Confirmation()
        {
            return View();
        }
	}
}
