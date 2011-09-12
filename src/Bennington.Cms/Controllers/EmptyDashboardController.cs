using System.Web.Mvc;

namespace Bennington.Cms.Controllers
{
    [Authorize]
    public class EmptyDashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}