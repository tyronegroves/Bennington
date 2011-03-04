using System;
using Bennington.Core.MenuSystem;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.PrincipalProvider.Registration
{
	public class ContentManagementMenuRegistration : IAmAnIconMenuItem, IServiceRegistration
	{
		public string Name 
		{ 
			get { return "User Management"; }
		}

		public string IconUrl
		{
			get { return "/Content/Canvas/UserManagementIcon.gif"; }
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
			locator.Register<IAmAnIconMenuItem, ContentManagementMenuRegistration>(Guid.NewGuid().ToString());
		}
	}
}