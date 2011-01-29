using System;
using MvcTurbine.ComponentModel;
using Paragon.ContentTree.Engines.Faq.Controllers;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.Engines.Faq.Registration
{
	public class FaqEngineServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, FaqController>(Guid.NewGuid().ToString());
		}
	}
}