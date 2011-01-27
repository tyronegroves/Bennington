using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;
using Paragon.ContentTree.Helpers;
using Paragon.ContentTree.Repositories;

namespace Paragon.ContentTree.ExampleEngineNodeProvider.Routing
{
	public class RegisterExampleEngineNodeProviderRoutes : IRouteRegistrator
	{
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
		private readonly ITreeNodeRepository treeNodeRepository;

		public RegisterExampleEngineNodeProviderRoutes(ITreeNodeRepository treeNodeRepository, ITreeNodeIdToUrl treeNodeIdToUrl)
		{
			this.treeNodeRepository = treeNodeRepository;
			this.treeNodeIdToUrl = treeNodeIdToUrl;
		}

		public void Register(RouteCollection routes)
		{
			foreach (var treeNode in treeNodeRepository.GetAll().Where(a => a.Type == typeof(ExampleEngineNodeProvider).AssemblyQualifiedName))
			{
				var url = treeNodeIdToUrl.GetUrlByTreeNodeId(treeNode.Id);
				if (url.StartsWith("/")) url = url.Substring(1);
				url = url + "/{action}";
				routes.Add(new Route
										(
											url,
											new RouteValueDictionary(new { controller = "ContactUs", action = "Index" }),
											new MvcRouteHandler()
										));
			}
		}
	}
}