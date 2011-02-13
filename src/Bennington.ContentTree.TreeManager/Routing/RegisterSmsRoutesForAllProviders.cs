using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;
using Paragon.ContentTree.Contexts;

namespace Paragon.ContentTree.TreeManager.Routing
{
	public class RegisterSmsRoutesForAllProviders : IRouteRegistrator
	{
		private readonly ITreeNodeProviderContext treeNodeProviderContext;

		public RegisterSmsRoutesForAllProviders(ITreeNodeProviderContext treeNodeProviderContext)
		{
			this.treeNodeProviderContext = treeNodeProviderContext;
		}

		public void Register(RouteCollection routes)
		{
			foreach (var provider in treeNodeProviderContext.GetAllTreeNodeProviders())
			{
				routes.MapRoute(
							null,
							string.Format("Manage/{0}/{{action}}", provider.ControllerToUseForCreation, provider.ActionToUseForCreation),
							new { controller = provider.ControllerToUseForCreation, action = provider.ActionToUseForCreation}
						);

				routes.MapRoute(
							null,
							string.Format("Manage/{0}/{{action}}", provider.ControllerToUseForModification, provider.ActionToUseForModification),
							new { controller = provider.ControllerToUseForModification, action = provider.ActionToUseForModification }
						);
			}
		}
	}
}