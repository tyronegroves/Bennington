using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTurbine.Routing;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.Routing.Routing.Helpers;

namespace Paragon.ContentTree.SectionNodeProvider.Routing
{
	public class RegisterSectionRoutes : IRouteRegistrator
	{
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
		private readonly ITreeNodeRepository treeNodeRepository;

		public RegisterSectionRoutes(ITreeNodeRepository treeNodeRepository, ITreeNodeIdToUrl treeNodeIdToUrl)
		{
			this.treeNodeRepository = treeNodeRepository;
			this.treeNodeIdToUrl = treeNodeIdToUrl;
		}

		public void Register(RouteCollection routes)
		{
			foreach (var treeNode in treeNodeRepository.GetAll().Where(a => a.Type == typeof(SectionNodeProvider).AssemblyQualifiedName))
			{
				var url = treeNodeIdToUrl.GetUrlByTreeNodeId(treeNode.Id);
				if (url.StartsWith("/")) url = url.Substring(1);
				routes.Add(new Route
										(
											url,
											new RouteValueDictionary(new { controller = "ContentTreeSection", action = "Index", treeNodeId = treeNode.Id }),
											new MvcRouteHandler()
										));
			}
		}
	}
}