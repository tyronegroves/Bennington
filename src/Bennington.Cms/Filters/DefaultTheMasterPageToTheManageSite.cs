using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.Attributes;
using MvcTurbine.ComponentModel;
using MvcTurbine.Web.Filters;

namespace Bennington.Cms.Filters
{
    public class RegisterTheDefaultMasterPageSetting : GlobalFilterRegistry
    {
        public RegisterTheDefaultMasterPageSetting()
        {
            AsGlobal<DefaultTheMasterPageToTheManageSite>();
        }
    }

    public class DefaultTheMasterPageToTheManageSite : ActionFilterAttribute
    {
        private readonly IServiceLocator serviceLocator;

        public DefaultTheMasterPageToTheManageSite(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (TheMasterPageShouldNotBeSet(filterContext))
                return;

            SetTheMasterPage(filterContext);

            base.OnActionExecuted(filterContext);
        }

        private bool TheMasterPageShouldNotBeSet(ActionExecutedContext filterContext)
        {
            return ThisIsNotTheMainPageThatIsBeingLoaded(filterContext)
                   || ThisIsNotAViewResult(filterContext)
                   || TheControllerIsMarkedAsOneThatWillNotUseTheDefaultMasterPage(filterContext)
                   || TheMasterPageHasBeenSetByTheAction(filterContext);
        }

        private void SetTheMasterPage(ActionExecutedContext filterContext)
        {
            var masterName = "~/Views/Shared/ManageSite.cshtml";
            var getDefaultCmsMasterPageName = serviceLocator.ResolveServices<IGetDefaultCmsMasterPageName>();
            if (getDefaultCmsMasterPageName.Count > 0)
            {
                masterName = getDefaultCmsMasterPageName.First().GetMasterName();
            }

            ((ViewResult)filterContext.Result).MasterName = masterName;
        }

        private bool TheMasterPageHasBeenSetByTheAction(ActionExecutedContext filterContext)
        {
            return string.IsNullOrEmpty((filterContext.Result as ViewResult).MasterName) == false;
        }

        private bool TheControllerIsMarkedAsOneThatWillNotUseTheDefaultMasterPage(ActionExecutedContext filterContext)
        {
            return filterContext.Controller.GetType().GetCustomAttributes(false).Any(x => x.GetType() == typeof(DoNotUseTheDefaultMasterPage));
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

    public interface IGetDefaultCmsMasterPageName
    {
        string GetMasterName();
    }
}