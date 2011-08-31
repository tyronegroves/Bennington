using System.Web.Mvc;
using System.Web.Security;

namespace Bennington.Cms.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}