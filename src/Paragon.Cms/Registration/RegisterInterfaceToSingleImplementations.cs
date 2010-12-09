using System.Collections.Generic;
using System.Reflection;
using Paragon.Core;
using Paragon.Core.Registration;

namespace Paragon.Cms.Registration
{
    public class RegisterInterfaceToSingleImplementations : InterfaceToSingleImplementationRegistrationConvention
    {
        protected override IEnumerable<Assembly> GetAssembliesToScan()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }
    }
}