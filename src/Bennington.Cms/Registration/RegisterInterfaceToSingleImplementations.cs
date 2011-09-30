using System.Collections.Generic;
using System.Reflection;
using Bennington.Cms.MenuSystem;
using Bennington.Core.Registration;

namespace Bennington.Cms.Registration
{
    public class RegisterInterfaceToSingleImplementations : InterfaceToSingleImplementationRegistrationConvention
    {
        public RegisterInterfaceToSingleImplementations()
        {
            InterfacesToIgnore.Add(typeof(IMenuRegistry));
        }

        protected override IEnumerable<Assembly> GetAssembliesToScan()
        {
            return new[] {Assembly.GetExecutingAssembly()};
        }
    }
}