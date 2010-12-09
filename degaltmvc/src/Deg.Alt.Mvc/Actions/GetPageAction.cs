using System.Collections.Generic;
using System.Linq;
using Deg.Alt.ContentProvider;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Actions
{
    public interface IGetPageAction
    {
        Page GetPage(string sectionId, string pageId);
        Page GetPage(string pageId);
    }

    public class GetPageAction : IGetPageAction
    {
        private readonly IServiceLocator serviceLocator;

        public GetPageAction(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public Page GetPage(string pageId)
        {
            return GetPages()
                .Where(x => string.Compare(x.Id, pageId, true) == 0)
                .FirstOrDefault();
        }

        public Page GetPage(string sectionId, string pageId)
        {
            return GetPages()
                .Where(x => string.Compare(x.SectionId, sectionId, true) == 0)
                .Where(x => string.Compare(x.Id, pageId, true) == 0)
                .FirstOrDefault();
        }

        private IEnumerable<Page> GetPages()
        {
            return serviceLocator.Resolve<IPageRepository>().GetPages();
        }
    }
}