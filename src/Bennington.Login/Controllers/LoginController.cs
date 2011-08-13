using System.Web.Mvc;
using Bennington.Cms.Attributes;
using Bennington.Login.Attributes;
using Bennington.Login.Models;

namespace Bennington.Login.Controllers
{
    [DoNotUseTheDefaultMasterPage]
    [DoNotRequireAuthentication]
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
