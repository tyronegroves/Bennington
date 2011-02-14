using System.Collections.Generic;
using System.Reflection;
using Bennington.Core.Registration;

namespace Bennington.ContentTree.Engines.Homepage.Registration
{
    public class RegisterInterfaceToSingleImplementations : InterfaceToSingleImplementationRegistrationConvention
    {
        protected override IEnumerable<Assembly> GetAssembliesToScan()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }
    }
}