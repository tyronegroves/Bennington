using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTreeNodeProvider.Context;
using Paragon.ContentTreeNodeProvider.Mappers;
using Paragon.ContentTreeNodeProvider.Models;
using Paragon.ContentTreeNodeProvider.Repositories;
using Paragon.ContentTreeNodeProvider.ViewModelBuilders;
using Paragon.ContentTreeNodeProvider.ViewModelBuilders.Helpers;

namespace Paragon.ContentTreeNodeProvider.Controllers
{
	public class ContentTreeNodeController : Controller
	{
		private readonly IContentTreeNodeRepository contentTreeNodeRepository;
		private readonly IContentTreeNodeToContentTreeNodeInputModelMapper contentTreeNodeToContentTreeNodeInputModelMapper;
		private readonly IContentTreeNodeInputModelToContentTreeNodeMapper contentTreeNodeInputModelToContentTreeNodeMapper;
		private readonly IContentTreeNodeContext contentTreeNodeContext;
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly ITreeNodeProviderContext treeNodeProviderContext;
		private readonly IContentTreeNodeDisplayViewModelBuilder contentTreeNodeDisplayViewModelBuilder;
		private readonly IRawUrlGetter rawUrlGetter;

		public ContentTreeNodeController(IContentTreeNodeRepository contentTreeNodeRepository, IContentTreeNodeToContentTreeNodeInputModelMapper contentTreeNodeToContentTreeNodeInputModelMapper, IContentTreeNodeInputModelToContentTreeNodeMapper contentTreeNodeInputModelToContentTreeNodeMapper, IContentTreeNodeContext contentTreeNodeContext, ITreeNodeRepository treeNodeRepository, ITreeNodeProviderContext treeNodeProviderContext,  IContentTreeNodeDisplayViewModelBuilder contentTreeNodeDisplayViewModelBuilder, IRawUrlGetter rawUrlGetter)
		{
			this.rawUrlGetter = rawUrlGetter;
			this.contentTreeNodeDisplayViewModelBuilder = contentTreeNodeDisplayViewModelBuilder;
			this.treeNodeProviderContext = treeNodeProviderContext;
			this.treeNodeRepository = treeNodeRepository;
			this.contentTreeNodeContext = contentTreeNodeContext;
			this.contentTreeNodeInputModelToContentTreeNodeMapper = contentTreeNodeInputModelToContentTreeNodeMapper;
			this.contentTreeNodeToContentTreeNodeInputModelMapper = contentTreeNodeToContentTreeNodeInputModelMapper;
			this.contentTreeNodeRepository = contentTreeNodeRepository;
		}

		public ActionResult Index()
		{
			return View("Index", contentTreeNodeDisplayViewModelBuilder.BuildViewModel(rawUrlGetter.GetRawUrl(), RouteData));
		}

		public ActionResult Delete(string treeNodeId)
		{
			contentTreeNodeContext.Delete(treeNodeId);
			return new RedirectToRouteResult(new RouteValueDictionary { { "controller", "ContentTree" }, { "action", "Index" }});
		}

		[HttpPost]
		public ActionResult Create(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			if (string.IsNullOrEmpty(contentTreeNodeInputModel.ContentItemId)) contentTreeNodeInputModel.ContentItemId = "Index";
			if (ModelState.IsValid == false)
				return View("Modify", new ContentTreeNodeViewModel()
				                      	{
				                      		ContentTreeNodeInputModel = contentTreeNodeInputModel,
											Action = "Create",
				                      	});
			contentTreeNodeInputModel.TreeNodeId = contentTreeNodeContext.CreateTreeNodeAndReturnTreeNodeId(contentTreeNodeInputModel);


			if (!string.IsNullOrEmpty(contentTreeNodeInputModel.Action))
			{
				if (contentTreeNodeInputModel.Action.ToLower() == "save and exit")
					return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTree"},
													{"action", "Index"},
			                                 	});
			}

			return new RedirectResult(GetRedirectUrlToModifyMethod(contentTreeNodeInputModel));
		}

		public ActionResult Create(string parentTreeNodeId, string providerType)
		{
			return View("Modify", new ContentTreeNodeViewModel()
			                      	{
										Action = "Create",
			                      		ContentTreeNodeInputModel = new ContentTreeNodeInputModel()
			                      		                        	{
			                      		                        		ParentTreeNodeId = parentTreeNodeId,
																		Type = providerType,
			                      		                        	}
			                      	});
		}

		[HttpPost]
		public ActionResult Modify(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			if (ModelState.IsValid == false)
				return View("Modify", new ContentTreeNodeViewModel() { Action = "Modify", ContentTreeNodeInputModel = contentTreeNodeInputModel });

			var contentTreeNodeFromRepository = contentTreeNodeRepository.GetAllContentTreeNodes().Where(a => a.TreeNodeId == contentTreeNodeInputModel.TreeNodeId && a.ContentItemId == contentTreeNodeInputModel.ContentItemId).FirstOrDefault();
			
			if (contentTreeNodeFromRepository == null)
			{
				contentTreeNodeFromRepository = contentTreeNodeInputModelToContentTreeNodeMapper.CreateInstance(contentTreeNodeInputModel);
				contentTreeNodeRepository.Create(contentTreeNodeFromRepository);
			}
			else
			{
				contentTreeNodeInputModelToContentTreeNodeMapper.LoadIntoInstance(contentTreeNodeInputModel, contentTreeNodeFromRepository);
				contentTreeNodeRepository.Update(contentTreeNodeFromRepository);
			}
			
			if (contentTreeNodeInputModel.Action != null)
			{
				if (contentTreeNodeInputModel.Action.ToLower() == "save and exit")
					return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTree"},
													{"action", "Index"},
			                                 	});
			}

			return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTreeNode"},
													{"action", "Modify"},
													{ "treeNodeId", contentTreeNodeInputModel == null ? "0" : contentTreeNodeInputModel.TreeNodeId },
													{ "contentItemId", contentTreeNodeInputModel == null ? "Index" : contentTreeNodeInputModel.ContentItemId },
			                                 	});
		}

		public ActionResult Modify(string treeNodeId, string contentItemId)
		{
			if (string.IsNullOrEmpty(contentItemId)) contentItemId = "Index";
			var contentTreeNode = contentTreeNodeRepository.GetAllContentTreeNodes().Where(a => a.TreeNodeId == treeNodeId && a.ContentItemId == contentItemId).FirstOrDefault();
			var contentTreeNodeInputModel = contentTreeNode == null ? new ContentTreeNodeInputModel()
			                		                        	{
			                		                        		TreeNodeId = treeNodeId,
																	ContentItemId = contentItemId,
			                		                        	} 
										: contentTreeNodeToContentTreeNodeInputModelMapper.CreateInstance(contentTreeNode);

			var viewModel = new ContentTreeNodeViewModel()
			                	{
									Action = "Modify",
			                		ContentTreeNodeInputModel = contentTreeNodeInputModel,
			                	};
			if (string.IsNullOrEmpty(viewModel.ContentTreeNodeInputModel.ContentItemId))
				viewModel.ContentTreeNodeInputModel.ContentItemId = "Index";

			return View("Modify", viewModel);
		}

		public ActionResult ContentItemNavigation(string treeNodeId)
		{
			var viewModel = new ContentItemNavigationViewModel()
			                	{
			                		TreeNodeId = treeNodeId
			                	};
			var treeNode = treeNodeRepository.GetAll().Where(a => a.Id == treeNodeId).FirstOrDefault();
			if (treeNode != null)
			{
				viewModel.ContentTreeNodeContentItems =
					treeNodeProviderContext.GetProviderByTypeName(treeNode.Type).ContentTreeNodeContentItems;
			}
			if ((viewModel.ContentTreeNodeContentItems == null) || (viewModel.ContentTreeNodeContentItems.Count() == 0)) return null;
			return View("ContentItemNavigation", viewModel);
		}

		private string GetRedirectUrlToModifyMethod(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			if (Url == null) return "/";
			return Url.Action("Modify", "ContentTreeNode", new { treeNodeId = contentTreeNodeInputModel == null ? "0" : contentTreeNodeInputModel.TreeNodeId });
		}
	}
}
