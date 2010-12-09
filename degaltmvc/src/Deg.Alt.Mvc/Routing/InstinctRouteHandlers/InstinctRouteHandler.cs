using System.Web;
using System.Web.Routing;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Routing.InstinctRouteHandlers
{
    public interface IInstinctRouteHandler : IRouteHandler
    {
    }

    public class InstinctRouteHandler : IInstinctRouteHandler
    {
        private readonly IServiceLocator serviceLocator;

        public InstinctRouteHandler(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        protected virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var instinctRouteHttpHandler = serviceLocator.Resolve<InstinctRouteHttpHandler>();
            instinctRouteHttpHandler.SetRequestContext(requestContext);
            return instinctRouteHttpHandler;
        }

        #region IRouteHandler Members

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return GetHttpHandler(requestContext);
        }

        #endregion
    }
}