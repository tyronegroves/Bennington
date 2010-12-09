using System;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Deg.Alt.Mvc.Routing.InstinctRouteHandlers
{
    public abstract class InstinctRouteHttpHandlerBase : IHttpHandler, IRequiresSessionState
    {
        public abstract string CalculateTheControllerName(RouteValueDictionary dictionary);

        public void SetRequestContext(RequestContext requestContext)
        {
            RequestContext = requestContext;
        }

        protected internal virtual void ProcessRequest(HttpContextBase httpContext)
        {
            AddVersionHeader(httpContext);

            // Get the controller type
            var controllerName = CalculateTheControllerName(RequestContext.RouteData.Values);

            // Instantiate the controller and call Execute
            var factory = ControllerBuilder.GetControllerFactory();
            var controller = factory.CreateController(RequestContext, controllerName);
            if (controller == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentUICulture,
                        string.Empty,
                        factory.GetType(),
                        controllerName));
            }
            try
            {
                controller.Execute(RequestContext);
            }
            finally
            {
                factory.ReleaseController(controller);
            }
        }

        #region copy/paste from mvc codebase

        private static readonly string MvcVersion = GetMvcVersionString();
        private ControllerBuilder controllerBuilder;
        public static readonly string MvcVersionHeaderName = "X-AspNetMvc-Version";

        protected virtual bool IsReusable
        {
            get { return false; }
        }

        bool IHttpHandler.IsReusable
        {
            get { return false; }
        }

        void IHttpHandler.ProcessRequest(HttpContext httpContext)
        {
            ProcessRequest(httpContext);
        }

        protected virtual void ProcessRequest(HttpContext httpContext)
        {
            HttpContextBase iHttpContext = new HttpContextWrapper(httpContext);
            ProcessRequest(iHttpContext);
        }

        protected internal virtual void AddVersionHeader(HttpContextBase httpContext)
        {
            httpContext.Response.AppendHeader(MvcVersionHeaderName, MvcVersion);
        }

        private static string GetMvcVersionString()
        {
            // DevDiv 216459:
            // This code originally used Assembly.GetName(), but that requires FileIOPermission, which isn't granted in
            // medium trust. However, Assembly.FullName *is* accessible in medium trust.
            return new AssemblyName(typeof (MvcHandler).Assembly.FullName).Version.ToString(2);
        }

        public RequestContext RequestContext { get; private set; }

        internal ControllerBuilder ControllerBuilder
        {
            get
            {
                if (controllerBuilder == null)
                {
                    controllerBuilder = ControllerBuilder.Current;
                }
                return controllerBuilder;
            }
            set { controllerBuilder = value; }
        }

        #endregion
    }
}