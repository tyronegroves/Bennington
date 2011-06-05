using System.Web;
using System.Web.Mvc;
using MvcTurbine.Web.Filters;

namespace Bennington.Cms
{
    public class RegisterTheDefaultMasterPageSetting : MvcTurbine.Web.Filters.GlobalFilterRegistry
    {
        public RegisterTheDefaultMasterPageSetting()
        {
            AsGlobal<DefaultTheMasterPageToTheManageSite>();
        }
    }

    public class DefaultTheMasterPageToTheManageSite : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction) return;

            var viewResult = filterContext.Result as ViewResult;
            if (viewResult == null) return;

            if (string.IsNullOrEmpty(viewResult.MasterName))
                viewResult.MasterName = "ManageSite";

            base.OnActionExecuted(filterContext);
        }
    }
}