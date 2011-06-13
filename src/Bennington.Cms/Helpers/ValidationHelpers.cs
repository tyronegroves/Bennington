using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bennington.Cms.Helpers
{
    public static class ValidationSummaryHelpers
    {
        public static Func<HtmlHelper, string, MvcHtmlString> GetValidationError = (htmlHelper, x) => htmlHelper.ValidationMessage(x);

        public static MvcHtmlString ValidationSummaryForForm(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ValidationSummaryForForm("");
        }

        private static string GetTheFirstPropertyWithAnError(ModelStateDictionary modelState, string prefixValue)
        {
            return modelState.First(x => x.Value.Errors.Count() > 0 && x.Key.StartsWith(prefixValue)).Key;
        }

        public static MvcHtmlString ValidationSummaryForForm(this HtmlHelper htmlHelper, string prefix)
        {
            var prefixValue = string.IsNullOrEmpty(prefix) ? prefix : prefix + ".";

            var modelState = htmlHelper.ViewData.ModelState;
            return AnErrorExists(modelState, prefixValue)
                       ? GetValidationError(htmlHelper, GetTheFirstPropertyWithAnError(modelState, prefixValue))
                       : MvcHtmlString.Create(@"<span id=""validationSummary"" class=""field-validation-error""></span>");
        }

        private static bool AnErrorExists(ModelStateDictionary modelState, string prefixValue)
        {
            var propertyWithError = modelState
                .FirstOrDefault(x =>
                                    {
                                        var count = x.Value.Errors.Count();
                                        var startsWith = x.Key.StartsWith(prefixValue);
                                        return count > 0 && startsWith;
                                    }).Key;

            return propertyWithError != null;
        }
    }
}