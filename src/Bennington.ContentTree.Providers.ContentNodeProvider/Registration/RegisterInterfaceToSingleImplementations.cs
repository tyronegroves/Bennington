using System.Collections.Generic;
using System.Reflection;
using Bennington.Core.Registration;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Registration
{
    public class RegisterInterfaceToSingleImplementations : InterfaceToSingleImplementationRegistrationConvention
    {
		public RegisterInterfaceToSingleImplementations()
		{
			
		}

        protected override IEnumerable<Assembly> GetAssembliesToScan()
        {
            return new[] { Assembly.GetExecutingAssembly() };
        }
    }
}