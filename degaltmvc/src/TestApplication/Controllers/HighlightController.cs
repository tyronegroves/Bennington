using System.Web.Mvc;
using Deg.Alt.ContentProvider;

namespace TestApplication.Controllers
{
    [HandleError]
    public class HighlightController : Controller
    {
        private readonly IPageRepository pageRepository;

        public HighlightController(IPageRepository pageRepository)
        {
            this.pageRepository = pageRepository;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Result()
        {
            return View("Result");
        }
    }
}