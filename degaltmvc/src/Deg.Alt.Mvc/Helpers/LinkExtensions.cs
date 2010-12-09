using System.Web.Routing;
using Deg.Alt.ContentProvider;

namespace System.Web.Mvc.Html
{
    public static class InstinctLinkExtensions
    {
        public static MvcHtmlString InstinctActionLink(this HtmlHelper htmlHelper, string linkText, Page page)
        {
            return htmlHelper.InstinctActionLink(linkText, page.Url);
        }

        public static MvcHtmlString InstinctActionLink(this HtmlHelper htmlHelper, string linkText, string url)
        {
            return InstinctActionLink(htmlHelper, linkText, url, new object());
        }

        public static MvcHtmlString InstinctActionLink(this HtmlHelper htmlHelper, string linkText, Page page, object htmlAttributes)
        {
            return htmlHelper.InstinctActionLink(linkText, page.Url, htmlAttributes);
        }

        public static MvcHtmlString InstinctActionLink(this HtmlHelper htmlHelper, string linkText, string url, object htmlAttributes)
        {
            var tagBuilder = CreateATagBuilderForThisLink(linkText, htmlAttributes, url);

            var linkHtml = tagBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(linkHtml);
        }

        private static TagBuilder CreateATagBuilderForThisLink(string linkText, object htmlAttributes, string url)
        {
            var tagBuilder = new TagBuilder("a"){
                                                    InnerHtml = (!String.IsNullOrEmpty(linkText)) ? HttpUtility.HtmlEncode(linkText) : String.Empty
                                                };

            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tagBuilder.MergeAttribute("href", url);
            return tagBuilder;
        }
    }
}