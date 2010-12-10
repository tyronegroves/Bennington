using MvcTurbine.ComponentModel;
using Paragon.ContentTree.Routing.Adapters;
using Paragon.ContentTree.Routing.Data;

namespace Paragon.ContentTree.Routing.Registration
{
	public class TreeNodeSummaryContextToContentTreeRepositoryAdapterServiceRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IContentTreeRepository, TreeNodeSummaryContextToContentTreeRepositoryAdapter>();
		}
	}
}