using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ContentTreeContactUsNodeProvider.Repositories;
using MvcTurbine.Routing;
using Paragon.ContentTree.Repositories;
using Paragon.Pages.Routing.Helpers;

namespace ContentTreeContactUsNodeProvider.Routing
{
	public class RegisterContactUsRoutes : IRouteRegistrator
	{
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
		private readonly ITreeNodeRepository treeNodeRepository;

		public RegisterContactUsRoutes(ITreeNodeRepository treeNodeRepository, ITreeNodeIdToUrl treeNodeIdToUrl)
		{
			this.treeNodeRepository = treeNodeRepository;
			this.treeNodeIdToUrl = treeNodeIdToUrl;
		}

		public void Register(RouteCollection routes)
		{
			foreach (var contactUsNode in treeNodeRepository.GetAll().Where(a => a.Type == typeof(ContentTreeContactUsNodeProvider).FullName))
			{
				var url = treeNodeIdToUrl.GetUrlByTreeNodeId(contactUsNode.Id);
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