using System.Web.Mvc;
using System.Web.UI.WebControls;
using Bennington.Core.List;

namespace SampleContentWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            var listViewModel = ListViewModelProviders.Providers.GetListViewModelForType(typeof(ListItem), ControllerContext);
            
            return View("Index", listViewModel);
        }

        public ActionResult About()
        {
            return View();
        }
    }

    public class NewsroomListItem
    {
        
    }
}
