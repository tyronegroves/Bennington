using System.Web.Mvc;

namespace Bennington.Core.Extensions
{
    public static class ViewEnginesExtensions
    {
        public static string FindPartialViewOrDefault(this ViewEngineCollection viewEngines, ControllerContext controllerContext, string partialViewName, string defaultPartialViewName)
        {
            return viewEngines.FindPartialView(controllerContext, partialViewName).View != null ? partialViewName : defaultPartialViewName;
        }

        public static bool PartialViewExists(this ViewEngineCollection viewEngines, ControllerContext controllerContext, string partialViewName)
        {
            return viewEngines.FindPartialView(controllerContext, partialViewName).View != null;
        }
    }
}