using System.Linq;
using System.Web.Mvc;

namespace DegDarwin.Helpers
{
    public static class EditorTemplateHelpers
    {
        public static MvcHtmlString RenderLabelForModel<TModel>(this HtmlHelper<TModel> html)
        {
            if (string.IsNullOrEmpty(html.GetCurrentPropertyName())) return new MvcHtmlString("");
            return MvcHtmlString.Create(string.Format("<span class=\"{2}\"><label for=\"{0}\">{1}:</label></span>",
                                                      html.GetCurrentPropertyName(),
                                                      GetTheCurrentDisplayName(html),
                                                      html.CurrentPropertyHasAnError() ? "field-validation-error" : "field-label"));
        }

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