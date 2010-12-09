using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcTurbine.ComponentModel;
using Paragon.Pages.Adapters;
using Paragon.Pages.Data;

namespace Paragon.Pages.Registration
{
	public class TreeNodeSummaryContextToContentTreeRepositoryAdapterServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IContentTreeRepository, TreeNodeSummaryContextToContentTreeRepositoryAdapter>();
		}
	}
}