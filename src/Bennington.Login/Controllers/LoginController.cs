using System.Web.Mvc;
using Bennington.Cms.Attributes;
using DegDarwin.Models;

namespace DegDarwin.Controllers
{
    [DoNotUseTheDefaultMasterPage]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", new LoginForm());
        }

        [HttpPost]
        public ActionResult Index(LoginForm loginForm)
        {
            return View("Index", new LoginForm());
        }
    }
}
