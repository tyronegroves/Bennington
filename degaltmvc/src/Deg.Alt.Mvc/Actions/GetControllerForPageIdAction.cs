using System.Linq;
using Deg.Alt.ContentProvider;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Actions
{
    public interface IGetControllerForPageIdAction
    {
        string GetController(string pageId);
    }

    public class GetControllerForPageIdAction : IGetControllerForPageIdAction
    {
        private readonly IServiceLocator serviceLocator;

        public GetControllerForPageIdAction(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public string GetController(string pageId)
        {
            var page = GetThePage(pageId);
            if (ThePageDoesNotExist(page))
                return null;
            return page.Controller;
        }

        private static bool ThePageDoesNotExist(Page page)
        {
            return page == null;
        }

        private Page GetThePage(string pageId)
        {
            return serviceLocator.Resolve<IPageRepository>().GetPages()
                .Where(x => string.Compare(x.Id, pageId, true) == 0)
                .FirstOrDefault();
        }
    }
}