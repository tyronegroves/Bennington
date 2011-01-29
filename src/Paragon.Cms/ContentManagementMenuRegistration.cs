using System;
using MvcTurbine.ComponentModel;
using Paragon.Core.MenuSystem;

namespace Paragon.ContentTree
{
	public class ContentManagementMenuRegistration : IAmAnIconMenuItem, IAmASectionMenuItem, IServiceRegistration
	{
		public string Name 
		{ 
			get { return "Content Tree"; }
		}
		
		public string IconUrl 
		{ 
			get { return "/Canvas/Top-Menu-Bg.gif"; }
		}
		
		public string Controller
		{
			get { return "ContentTree"; }
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
			locator.Register<IAmASectionMenuItem, ContentManagementMenuRegistration>(Guid.NewGuid().ToString());
		}
	}
}