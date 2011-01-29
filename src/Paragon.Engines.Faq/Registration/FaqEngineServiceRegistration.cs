using System;
using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;
using Paragon.Engines.Faq.Controllers;

namespace Paragon.Engines.Faq.Registration
{
	public class FaqEngineServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, FaqController>(Guid.NewGuid().ToString());
		}
	}
}