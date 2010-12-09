using System.Linq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Mappers;

namespace Deg.Alt.Mvc.Actions
{
    public interface IGetPageForPageLocationAction
    {
        Page GetPage(PageLocation pageLocation);
    }

    public class GetPageForPageLocationAction : IGetPageForPageLocationAction
    {
        private readonly IPageRepository pageRepository;
        private readonly ISectionRepository sectionRepository;

        public GetPageForPageLocationAction(IPageRepository pageRepository, ISectionRepository sectionRepository)
        {
            this.pageRepository = pageRepository;
            this.sectionRepository = sectionRepository;
        }

        public Page GetPage(PageLocation pageLocation)
        {
            var page = GetPageByPageId(pageLocation);

            if (ThePageExists(page))
                return page;

            return GetTheDefaultPageForTheCurrentSection(pageLocation);
        }

        private Page GetTheDefaultPageForTheCurrentSection(PageLocation pageLocation)
        {
            var section = GetSection(pageLocation);

            if (TheSectionDoesNotExist(section))
                return null;

            return GetTheDefaultPageForTheSection(section);
        }

        private Page GetTheDefaultPageForTheSection(Section section)
        {
            var pageKey = section.DefaultPageKey;
            return pageRepository.GetPages()
                .Where(x => x.Key == pageKey)
                .FirstOrDefault();
        }

        private static bool TheSectionDoesNotExist(Section section)
        {
            return section == null;
        }

        private Section GetSection(PageLocation pageLocation)
        {
            return sectionRepository.GetSections()
                .Where(x => string.Compare(x.Id, pageLocation.SectionId, true) == 0)
                .FirstOrDefault();
        }

        private static bool ThePageExists(Page page)
        {
            return page != null;
        }

        private Page GetPageByPageId(PageLocation pageLocation)
        {
            return pageRepository.GetPages()
                .Where(x => string.Compare(x.Id, pageLocation.PageId, true) == 0)
                .FirstOrDefault();
        }
    }
}