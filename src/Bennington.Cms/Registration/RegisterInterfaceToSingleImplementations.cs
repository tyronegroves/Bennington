using System.Collections.Generic;
using System.Reflection;
using Bennington.Cms.Models;
using Bennington.Core.Registration;

namespace Bennington.Cms.Registration
{
    public class RegisterInterfaceToSingleImplementations : InterfaceToSingleImplementationRegistrationConvention
    {
        public RegisterInterfaceToSingleImplementations()
        {
            InterfacesToIgnore.Add(typeof (ISectionMenuItemRegistry));
        }

        protected override IEnumerable<Assembly> GetAssembliesToScan()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }
    }
}