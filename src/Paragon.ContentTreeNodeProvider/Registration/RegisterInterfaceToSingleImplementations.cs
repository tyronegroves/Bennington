using System.Collections.Generic;
using System.Reflection;
using Paragon.Core.Registration;

namespace Paragon.ContentTree.ContentNodeProvider.Registration
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