using System.Linq;
using System.Web.Mvc;

namespace Bennington.Cms.Helpers
{
    public static class EditorTemplateHelpers
    {
        public static string GetTheCurrentDisplayName<TModel>(HtmlHelper<TModel> html)
        {
            return html.ViewData.ModelMetadata.DisplayName ?? html.ViewData.ModelMetadata.PropertyName;
        }

        public static bool CurrentPropertyHasAnError(this HtmlHelper htmlHelper)
        {
            if (htmlHelper.ViewData.ModelState.ContainsKey(htmlHelper.GetCurrentPropertyName()) == false) return false;
            return htmlHelper.ViewData.ModelState[htmlHelper.GetCurrentPropertyName()].Errors.Count() > 0;
        }

        public static string GetCurrentPropertyName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix;
        }
    }
}