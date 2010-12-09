using System.Web;
using System.Web.Routing;
using Deg.Alt.Mvc.Actions;
using Deg.Alt.Mvc.Mappers;

namespace Deg.Alt.Mvc.Routing.RouteConstraints
{
    public interface IContentPageInSectionRouteConstraint : IRouteConstraint
    {
    }

    public class ContentPageInSectionRouteConstraint : IContentPageInSectionRouteConstraint
    {
        private readonly IGetPageAction getPageAction;
        private readonly IRouteValueDictionaryToPageLocationMapper routeValueDictionaryToPageLocationMapper;

        public ContentPageInSectionRouteConstraint(IGetPageAction getPageAction, IRouteValueDictionaryToPageLocationMapper routeValueDictionaryToPageLocationMapper)
        {
            this.getPageAction = getPageAction;
            this.routeValueDictionaryToPageLocationMapper = routeValueDictionaryToPageLocationMapper;
        }

        public virtual bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var pageLocation = routeValueDictionaryToPageLocationMapper.CreateInstance(values);
            return getPageAction.GetPage(pageLocation.SectionId, pageLocation.PageId) != null;
        }
    }
}