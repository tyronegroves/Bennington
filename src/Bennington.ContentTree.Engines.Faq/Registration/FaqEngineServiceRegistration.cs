using System;
using Bennington.ContentTree.Engines.Faq.Controllers;
using Bennington.ContentTree.TreeNodeExtensionProvider;
using MvcTurbine.ComponentModel;

namespace Bennington.ContentTree.Engines.Faq.Registration
{
	public class FaqEngineServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, FaqController>(Guid.NewGuid().ToString());
		}
	}
}