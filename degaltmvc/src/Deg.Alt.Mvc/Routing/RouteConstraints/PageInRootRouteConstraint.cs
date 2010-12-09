using System.Web;
using System.Web.Routing;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Actions;
using Deg.Alt.Mvc.Mappers;

namespace Deg.Alt.Mvc.Routing.RouteConstraints
{
    public interface IPageInRootRouteConstraint : IRouteConstraint
    {
    }

    public class PageInRootRouteConstraint : IPageInRootRouteConstraint
    {
        private readonly IGetPageForRoutingAction getPageForRoutingAction;
        private readonly IRouteValueDictionaryToPageLocationMapper routeValueDictionaryToPageLocationMapper;

        public PageInRootRouteConstraint(IGetPageForRoutingAction getPageForRoutingAction,
                                         IRouteValueDictionaryToPageLocationMapper routeValueDictionaryToPageLocationMapper)
        {
            this.getPageForRoutingAction = getPageForRoutingAction;
            this.routeValueDictionaryToPageLocationMapper = routeValueDictionaryToPageLocationMapper;
        }

        public virtual bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var pageLocation = GetTheCurrentLocation(values);

            if (TheUserIsCurrentlyInASection(pageLocation))
                return false;

            var page = GetTheCurrentPage(pageLocation);

            return ThePageIsValid(page);
        }

        private static bool ThePageIsValid(Page page)
        {
            return ThePageExists(page);
        }

        private static bool ThePageIsNotInASection(Page page)
        {
            return string.IsNullOrEmpty(page.SectionId);
        }

        private static bool ThePageExists(Page page)
        {
            return page != null;
        }

        private Page GetTheCurrentPage(PageLocation pageLocation)
        {
            return getPageForRoutingAction.GetPage(pageLocation.PageId);
        }

        private PageLocation GetTheCurrentLocation(RouteValueDictionary values)
        {
            return routeValueDictionaryToPageLocationMapper.CreateInstance(values);
        }

        private static bool TheUserIsCurrentlyInASection(PageLocation pageLocation)
        {
            return string.IsNullOrEmpty(pageLocation.SectionId) == false;
        }
    }
}