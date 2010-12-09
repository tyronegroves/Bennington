using System.Linq;
using System.Web.Routing;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Mappers;

namespace Deg.Alt.Mvc.Routing.InstinctRouteHandlers
{
    public class InstinctSectionRouteHttpHandler : InstinctRouteHttpHandlerBase
    {
        private readonly IRouteValueDictionaryToPageLocationMapper routeValueDictionaryToPageLocationMapper;
        private readonly ISectionRepository sectionRepository;

        public InstinctSectionRouteHttpHandler(IRouteValueDictionaryToPageLocationMapper routeValueDictionaryToPageLocationMapper,
                                               ISectionRepository sectionRepository)
        {
            this.routeValueDictionaryToPageLocationMapper = routeValueDictionaryToPageLocationMapper;
            this.sectionRepository = sectionRepository;
        }

        public override string CalculateTheControllerName(RouteValueDictionary dictionary)
        {
            var pageLocation = GetThePageLocation(dictionary);

            var page = GetThePage(pageLocation);

            if (NoPageExists(page))
                return string.Empty;

            SetRouteValueDictionaryValues(dictionary, page);

            return page.Controller;
        }

        private static bool NoPageExists(Page page)
        {
            return page == null;
        }

        private static void SetRouteValueDictionaryValues(RouteValueDictionary dictionary, Page page)
        {
            dictionary["controller"] = page.Controller;
            dictionary["pageId"] = page.Id;
        }

        private Page GetThePage(PageLocation pageLocation)
        {
            var section = sectionRepository.GetSections()
                .Where(x => x.Id == pageLocation.SectionId)
                .FirstOrDefault();
            if (section == null) return null;

            var page = section.Pages.Where(x => x.Key == section.DefaultPageKey).FirstOrDefault();
            if (page == null)
                page = section.Pages.FirstOrDefault();
            return page;
        }

        private PageLocation GetThePageLocation(RouteValueDictionary dictionary)
        {
            return routeValueDictionaryToPageLocationMapper.CreateInstance(dictionary);
        }
    }
}