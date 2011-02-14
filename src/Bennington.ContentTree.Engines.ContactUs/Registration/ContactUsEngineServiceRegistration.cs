using System;
using Bennington.ContentTree.Engines.ContactUs.Controllers;
using Bennington.ContentTree.TreeNodeExtensionProvider;
using MvcTurbine.ComponentModel;

namespace Bennington.ContentTree.Engines.ContactUs.Registration
{
	public class ContactUsEngineServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, ContactUsController>(Guid.NewGuid().ToString());
		}
	}
}