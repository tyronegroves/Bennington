using System.Linq;
using Deg.Alt.ContentProvider;
using Deg.Alt.ContentProvider.RelatedItemReaders;
using Deg.Alt.Mvc.Mappers;

namespace Deg.Alt.Mvc.Actions
{
    public interface IGetContentItemForPageLocationAction
    {
        ContentItem GetContentItem(PageLocation pageLocation);
    }

    public class GetContentItemForPageLocationAction : IGetContentItemForPageLocationAction
    {
        private readonly IGetPageForPageLocationAction getPageForPageLocationAction;

        public GetContentItemForPageLocationAction(IGetPageForPageLocationAction getPageForPageLocationAction)
        {
            this.getPageForPageLocationAction = getPageForPageLocationAction;
        }

        public ContentItem GetContentItem(PageLocation pageLocation)
        {
            var page = GetThePageForThePageLocation(pageLocation);
            if (NoDataExists(page))
                return null;

            return GetTheContentItemForThisPageLocation(pageLocation, page);
        }

        private static ContentItem GetTheContentItemForThisPageLocation(PageLocation pageLocation, Page page)
        {
            return page.RelatedItems
                .OfType<ContentItem>()
                .Where(x => string.Compare(x.Id, pageLocation.Step, true) == 0)
                .FirstOrDefault();
        }

        private static bool NoDataExists(Page page)
        {
            return ThePageDoesNotExist(page) || ThePageHasNoRelatedItems(page);
        }

        private static bool ThePageHasNoRelatedItems(Page page)
        {
            return page.RelatedItems == null;
        }

        private static bool ThePageDoesNotExist(Page page)
        {
            return page == null;
        }

        private Page GetThePageForThePageLocation(PageLocation pageLocation)
        {
            return getPageForPageLocationAction.GetPage(pageLocation);
        }
    }
}