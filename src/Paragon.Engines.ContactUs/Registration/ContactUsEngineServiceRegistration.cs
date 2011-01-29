using System;
using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;
using Paragon.Engines.ContactUs.Controllers;

namespace Paragon.Engines.ContactUs.Registration
{
	public class ContactUsEngineServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, ContactUsController>(Guid.NewGuid().ToString());
		}
	}
}