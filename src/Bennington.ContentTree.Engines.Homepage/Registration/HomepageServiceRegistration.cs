using System;
using Bennington.ContentTree.Engines.Homepage.Controllers;
using Bennington.ContentTree.TreeNodeExtensionProvider;
using MvcTurbine.ComponentModel;

namespace Bennington.ContentTree.Engines.Homepage.Registration
{
	public class HomepageServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, HomepageController>(Guid.NewGuid().ToString());
		}
	}
}