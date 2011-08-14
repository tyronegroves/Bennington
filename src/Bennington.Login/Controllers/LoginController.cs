using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.Attributes;
using Bennington.Login.Attributes;
using Bennington.Login.Models;
using MvcTurbine.MembershipProvider;

namespace Bennington.Login.Controllers
{
    [DoNotUseTheDefaultMasterPage]
    [DoNotRequireAuthentication]
    public class LoginController : Controller
    {
        private const string InvalidLoginFormMessage = "Login Failed. Please try again.";
        private readonly IMembershipService membershipService;

        public LoginController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }

        public ActionResult Index()
        {
            return View("Index", new LoginForm());
        }

        [HttpPost]
        public ActionResult Index(LoginForm loginForm)
        {
            return ThisIsAValidLogin(loginForm)
                       ? LoginAndRedirect(loginForm)
                       : ReturnTheFormAsAnError(loginForm);
        }

        private ActionResult ReturnTheFormAsAnError(LoginForm loginForm)
        {
            foreach (var field in typeof (LoginForm).GetProperties().Select(x => x.Name))
                ModelState.AddModelError(field, InvalidLoginFormMessage);
            return View("Index", loginForm);
        }

        private ActionResult LoginAndRedirect(LoginForm loginForm)
        {
            membershipService.LogInAsUser(loginForm.Username, loginForm.Password);
            return Redirect("/MANAGE");
        }

        private bool ThisIsAValidLogin(LoginForm loginForm)
        {
            return membershipService.ValidateUser(loginForm.Username, loginForm.Password);
        }
    }
}