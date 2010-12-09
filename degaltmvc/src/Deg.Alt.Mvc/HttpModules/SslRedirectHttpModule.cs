using System;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Deg.Alt.ContentProvider;

namespace Deg.Alt.Mvc.HttpModules
{
    public class SslRedirectHttpModule : IHttpModule
    {
        private readonly IPageRepository pageRepository;

        public SslRedirectHttpModule(IPageRepository pageRepository)
        {
            this.pageRepository = pageRepository;
        }

        public void Init(HttpApplication context)
        {
            context.PostRequestHandlerExecute += Handle;
        }

        public void Handle(Object application, EventArgs eventArgs)
        {
            var httpApplication = application as HttpApplication;
            if (httpApplication == null) return;

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));

            if (routeData == null) return;
            if (routeData.Values.ContainsKey("pageId"))
            {
                var pageId = routeData.Values["pageId"].ToString();
                var pages = pageRepository.GetPages().ToArray();
                var page = pages.Where(a => a.Id == pageId).FirstOrDefault();
                if (page == null) return;
                if (page.SecurePage)
                {
                    if (httpApplication.Request.ServerVariables["SERVER_PORT_SECURE"] != "1")
                    {
                        var uri = httpApplication.Request.Url;
                        var newUrl = uri.OriginalString.Replace("http:", "https:").Replace(":80", string.Empty);
                        HttpContext.Current.Response.Redirect(newUrl);
                    }
                }
                else
                {
                    if (httpApplication.Request.ServerVariables["SERVER_PORT_SECURE"] == "1")
                    {
                        var uri = httpApplication.Request.Url;
                        var newUrl = uri.OriginalString.Replace("https:", "http:").Replace(":443", string.Empty);
                        HttpContext.Current.Response.Redirect(newUrl);
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}