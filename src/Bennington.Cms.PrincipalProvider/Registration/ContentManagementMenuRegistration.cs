using System;
using Bennington.Core.MenuSystem;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.PrincipalProvider.Registration
{
	public class ContentManagementMenuRegistration : IAmASectionMenuItem, IServiceRegistration
	{
		public string Name 
		{ 
			get { return "User Management"; }
		}
		
		public string Controller
		{
			get { return "User"; }
		}
		
		public string Action 
		{ 
			get { return "Index"; }
		}
		
		public object RouteValues 
		{ 
			get { return null; }
		}

		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmASectionMenuItem, ContentManagementMenuRegistration>(Guid.NewGuid().ToString());
		}
	}
}