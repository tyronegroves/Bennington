using Deg.Alt.Mvc.Helpers;
using MvcTurbine.ComponentModel;

namespace Deg.Alt.Mvc.Registration
{
    public class HelperRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            locator.Register(typeof (ITagReplacer), typeof (TagReplacer));
        }
    }
}