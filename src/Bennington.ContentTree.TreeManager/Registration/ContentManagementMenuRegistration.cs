using System;
using Bennington.Core.MenuSystem;
using MvcTurbine.ComponentModel;

namespace Bennington.ContentTree.TreeManager.Registration
{
	public class ContentManagementMenuRegistration : IAmAnIconMenuItem, IServiceRegistration
	{
		public string Name 
		{ 
			get { return "Content Tree"; }
		}
		
		public string IconUrl 
		{
			get { return "/MANAGE/Content/Canvas/ContentTreeManagementIcon.gif"; }
		}
		
		public string Controller
		{
			get { return "TreeManager"; }
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