using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.Login.Attributes;
using MvcTurbine.Web.Filters;

namespace Bennington.Login.Filters
{
    public class RegisterTheDefaultMasterPageSetting : GlobalFilterRegistry
    {
        public RegisterTheDefaultMasterPageSetting()
        {
            AsGlobal<RedirectToTheLoginPageIfNotLoggedIn>();
        }
    }

    public class RedirectToTheLoginPageIfNotLoggedIn : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (NoLoginRedirectionIsNecessary(filterContext))
                return;

            RedirectToTheLoginPage(filterContext);

            base.OnActionExecuted(filterContext);
        }

        private void RedirectToTheLoginPage(ActionExecutedContext filterContext)
        {
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("controller", "Login");
            routeValueDictionary.Add("action", "Index");
            filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
        }

        private bool NoLoginRedirectionIsNecessary(ActionExecutedContext filterContext)
        {
            return TheUserIsAlreadyLoggedIn()
                   || ThisIsNotTheMainPageThatIsBeingLoaded(filterContext)
                   || ThisIsNotAViewResult(filterContext)
                   || TheControllerIsMarkedAsOneThatWillNotUseTheDefaultMasterPage(filterContext)
                   || TheMasterPageHasBeenSetByTheAction(filterContext);
        }

        private bool TheUserIsAlreadyLoggedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        private static void SetTheMasterPage(ActionExecutedContext filterContext)
        {
            ((ViewResult) filterContext.Result).MasterName = "ManageSite";
        }

        private bool TheMasterPageHasBeenSetByTheAction(ActionExecutedContext filterContext)
        {
            return string.IsNullOrEmpty((filterContext.Result as ViewResult).MasterName) == false;
        }

        private bool TheControllerIsMarkedAsOneThatWillNotUseTheDefaultMasterPage(ActionExecutedContext filterContext)
        {
            return filterContext.Controller.GetType().GetCustomAttributes(false).Any(x => x.GetType() == typeof (DoNotRequireAuthentication));
        }

        private bool ThisIsNotAViewResult(ActionExecutedContext filterContext)
        {
            return filterContext.Result as ViewResult == null;
        }

        private bool ThisIsNotTheMainPageThatIsBeingLoaded(ActionExecutedContext filterContext)
        {
            return filterContext.IsChildAction;
        }
    }
}