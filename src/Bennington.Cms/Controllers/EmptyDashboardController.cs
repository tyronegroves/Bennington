using System.Web.Mvc;

namespace Bennington.Cms.Controllers
{
    public class EmptyDashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}