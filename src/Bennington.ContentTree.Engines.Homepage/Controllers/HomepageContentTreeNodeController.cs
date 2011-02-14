using System.Web.Mvc;
using System.Web.Routing;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Providers.ContentNodeProvider.Context;
using Bennington.ContentTree.Providers.ContentNodeProvider.Controllers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Bennington.ContentTree.Providers.ContentNodeProvider.ViewModelBuilders;
using Bennington.ContentTree.Providers.ContentNodeProvider.ViewModelBuilders.Helpers;
using Bennington.ContentTree.Repositories;
using Bennington.Core.Helpers;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Engines.Homepage.Controllers
{
	public class HomepageContentTreeNodeController : ContentTreeNodeController
	{
		public HomepageContentTreeNodeController(IContentTreeNodeVersionContext contentTreeNodeVersionContext, IContentTreeNodeToContentTreeNodeInputModelMapper contentTreeNodeToContentTreeNodeInputModelMapper, IContentTreeNodeInputModelToContentTreeNodeMapper contentTreeNodeInputModelToContentTreeNodeMapper, IContentTreeNodeContext contentTreeNodeContext, ITreeNodeRepository treeNodeRepository, ITreeNodeProviderContext treeNodeProviderContext, IContentTreeNodeDisplayViewModelBuilder contentTreeNodeDisplayViewModelBuilder, IRawUrlGetter rawUrlGetter, ICommandBus commandBus, IGuidGetter guidGetter) : base(contentTreeNodeVersionContext, contentTreeNodeToContentTreeNodeInputModelMapper, contentTreeNodeInputModelToContentTreeNodeMapper, contentTreeNodeContext, treeNodeRepository, treeNodeProviderContext, contentTreeNodeDisplayViewModelBuilder, rawUrlGetter, commandBus, guidGetter)
		{
		}

		public override ActionResult Create(string parentTreeNodeId, string providerType)
		{
			return View("Modify", new ContentTreeNodeViewModel()
			{
				Action = "Create",
				ContentTreeNodeInputModel = new ContentTreeNodeInputModel()
				{
					UrlSegment = "Index_",
					ParentTreeNodeId = parentTreeNodeId,
					Type = providerType,
				}
			});
		}

		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		public override ActionResult Create(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			var result = base.Create(contentTreeNodeInputModel);
			if ((result as RedirectResult) == null) return result;

			return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "HomepageContentTreeNode"},
													{"action", "Modify"},
													{ "treeNodeId", contentTreeNodeInputModel == null ? "0" : contentTreeNodeInputModel.TreeNodeId },
													{ "contentItemId", contentTreeNodeInputModel == null ? "Index" : contentTreeNodeInputModel.Action },
			                                 	});

		}

		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		public override ActionResult Modify(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			var result = base.Modify(contentTreeNodeInputModel);
			if ((result as RedirectToRouteResult) == null) return result;
			
			return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "HomepageContentTreeNode"},
													{"action", "Modify"},
													{ "treeNodeId", contentTreeNodeInputModel == null ? "0" : contentTreeNodeInputModel.TreeNodeId },
													{ "contentItemId", contentTreeNodeInputModel == null ? "Index" : contentTreeNodeInputModel.Action },
			                                 	});
		}
	}
}
