using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Paragon.Core.Registration;

namespace Paragon.Pages.Registration
{
	public class RegisterInterfaceToSingleImplementations : InterfaceToSingleImplementationRegistrationConvention
	{
		protected override IEnumerable<Assembly> GetAssembliesToScan()
		{
			return new[] { Assembly.GetExecutingAssembly() };
		}
	}
}