using System.Web.Mvc;

namespace TestApplication.Controllers
{
    [HandleError]
    public class IndexController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult About()
        {
            return View("About");
        }
    }
}