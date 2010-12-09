using Deg.Alt.Mvc.Actions;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Registration
{
    public class ActionRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register(typeof (IGetContentItemForPageLocationAction), typeof (GetContentItemForPageLocationAction));
            locator.Register(typeof (IGetControllerForPageIdAction), typeof (GetControllerForPageIdAction));
            locator.Register(typeof (IGetPageAction), typeof (GetPageAction));
            locator.Register(typeof (IGetPageForPageLocationAction), typeof (GetPageForPageLocationAction));
            locator.Register(typeof (IGetPageForRoutingAction), typeof (GetPageForRoutingAction));
        }
    }
}