using System;
using MvcTurbine.ComponentModel;
using Paragon.ContentTree.Engines.Homepage.Controllers;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.Engines.Homepage.Registration
{
	public class HomepageServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, HomepageController>(Guid.NewGuid().ToString());
		}
	}
}