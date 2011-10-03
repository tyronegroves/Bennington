using System;
using Bennington.Core.MenuSystem;
using MvcTurbine.ComponentModel;

namespace SampleCmsWebsite.Registration
{
    public class LocationSectionMenuRegistration : IAmASectionMenuItem, IServiceRegistration
    {
        public string Name
        {
            get { return "Locations"; }
        }

        public string Controller
        {
            get { return "Location"; }
        }

        public string Action
        {
            get { return "Index"; }
        }

        public object RouteValues
        {
            get { return new object(); }
        }

        public void Register(IServiceLocator locator)
        {
            locator.Register<IAmASectionMenuItem, LocationSectionMenuRegistration>(Guid.NewGuid().ToString());
        }
    }
}