using System.Collections.Generic;
using Bennington.Cms.Models;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using MvcTurbine.Web;

namespace SampleApp
{
    public class TestingRegistry : Bennington.Cms.Models.ISectionMenuItemRegistry
    {
        public IEnumerable<SectionMenuItem> GetItems()
        {
            return new[] {new SectionMenuItem(), new SectionMenuItem()};
        }
    }

    public class MvcApplication : TurbineApplication
    {
        static MvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
        }
    }
}