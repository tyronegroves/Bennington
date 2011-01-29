using System;
using MvcTurbine.ComponentModel;
using Paragon.ContentTree.Engines.ContactUs.Controllers;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.Engines.ContactUs.Registration
{
	public class ContactUsEngineServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, ContactUsController>(Guid.NewGuid().ToString());
		}
	}
}