using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.ViewModelBuilders;
using Paragon.ContentTree.ContentNodeProvider.ViewModelBuilders.Helpers;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Repositories;
using Paragon.Core.Helpers;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Engines.Homepage.Controllers
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
		public override ActionResult Create(ContentNodeProvider.Models.ContentTreeNodeInputModel contentTreeNodeInputModel)
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
		public override ActionResult Modify(ContentNodeProvider.Models.ContentTreeNodeInputModel contentTreeNodeInputModel)
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
