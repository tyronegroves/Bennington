using System.Linq;
using Deg.Alt.ContentProvider;
using Deg.Alt.Mvc.Routing;

namespace Deg.Alt.Mvc.Actions
{
    public interface IGetPageForRoutingAction
    {
        Page GetPage(string pageId);
        Page GetPage(string sectionId, string pageId);
    }

    public class GetPageForRoutingAction : IGetPageForRoutingAction
    {
        private readonly IGetPageAction getPageAction;
        private readonly IControllersExcludedFromRoutingRegistry controllersExcludedFromRoutingRegistry;

        public GetPageForRoutingAction(IGetPageAction getPageAction,
                                       IControllersExcludedFromRoutingRegistry controllersExcludedFromRoutingRegistry)
        {
            this.getPageAction = getPageAction;
            this.controllersExcludedFromRoutingRegistry = controllersExcludedFromRoutingRegistry;
        }

        public Page GetPage(string pageId)
        {
            var page = getPageAction.GetPage(pageId);

            return ReturnPageFromControllerFilter(page);
        }

        public Page GetPage(string sectionId, string pageId)
        {
            var page = getPageAction.GetPage(sectionId, pageId);

            return ReturnPageFromControllerFilter(page);
        }

        private Page ReturnPageFromControllerFilter(Page page)
        {
            if (page == null)
                return null;

            if (controllersExcludedFromRoutingRegistry.GetExcludedControllers().Contains(page.Controller))
                return null;

            return page;
        }
    }
}