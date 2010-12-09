using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Paragon.ContentTreeSectionNodeProvider.Context;
using Paragon.ContentTreeSectionNodeProvider.Mappers;
using Paragon.ContentTreeSectionNodeProvider.Models;
using Paragon.ContentTreeSectionNodeProvider.Repositories;

namespace Paragon.ContentTreeSectionNodeProvider.Controllers
{
	public class ContentTreeSectionNodeController : Controller
	{
		private readonly IContentTreeSectionNodeRepository ContentTreeSectionNodeRepository;
		private readonly IContentTreeSectionNodeToContentTreeSectionInputModelMapper contentTreeSectionNodeToContentTreeSectionInputModelMapper;
		private readonly IContentTreeSectionInputModelToContentTreeSectionNodeMapper ContentTreeSectionInputModelToContentTreeSectionNodeMapper;
		private readonly IContentTreeSectionNodeContext ContentTreeSectionNodeContext;

		public ContentTreeSectionNodeController(IContentTreeSectionNodeRepository ContentTreeSectionNodeRepository, IContentTreeSectionNodeToContentTreeSectionInputModelMapper ContentTreeSectionNodeToContentTreeSectionInputModelMapper, IContentTreeSectionInputModelToContentTreeSectionNodeMapper ContentTreeSectionInputModelToContentTreeSectionNodeMapper, IContentTreeSectionNodeContext ContentTreeSectionNodeContext)
		{
			this.ContentTreeSectionNodeContext = ContentTreeSectionNodeContext;
			this.ContentTreeSectionInputModelToContentTreeSectionNodeMapper = ContentTreeSectionInputModelToContentTreeSectionNodeMapper;
			this.contentTreeSectionNodeToContentTreeSectionInputModelMapper = ContentTreeSectionNodeToContentTreeSectionInputModelMapper;
			this.ContentTreeSectionNodeRepository = ContentTreeSectionNodeRepository;
		}

		public ActionResult Delete(string treeNodeId)
		{
			ContentTreeSectionNodeContext.Delete(treeNodeId);
			return new RedirectToRouteResult(new RouteValueDictionary { { "controller", "ContentTree" }, { "action", "Index" } });
		}

		[HttpPost]
		public ActionResult Create(ContentTreeSectionInputModel contentTreeSectionInputModel)
		{
			if (ModelState.IsValid == false)
				return View("Modify", new ContentTreeSectionNodeViewModel()
				                      	{
				                      		ContentTreeSectionInputModel = contentTreeSectionInputModel,
											Action = "Create",
				                      	});
			
			contentTreeSectionInputModel.TreeNodeId = ContentTreeSectionNodeContext.CreateTreeNodeAndReturnTreeNodeId(contentTreeSectionInputModel);

			if (contentTreeSectionInputModel.Action.ToLower() == "save and exit")
				return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTree"},
													{"action", "Index"},
			                                 	});

			return new RedirectResult(GetRedirectUrlToModifyMethod(contentTreeSectionInputModel));
		}

		public ActionResult Create(string parentTreeNodeId)
		{
			return View("Modify", new ContentTreeSectionNodeViewModel()
			                      	{
										Action = "Create",
			                      		ContentTreeSectionInputModel = new ContentTreeSectionInputModel()
			                      		                        	{
			                      		                        		ParentTreeNodeId = parentTreeNodeId,
			                      		                        	}
			                      	});
		}

		[HttpPost]
		public ActionResult Modify(ContentTreeSectionInputModel contentTreeSectionInputModel)
		{
			if (ModelState.IsValid == false)
				return View("Modify", new ContentTreeSectionNodeViewModel() { Action = "Modify", ContentTreeSectionInputModel = contentTreeSectionInputModel });

			var contentTreeSectionNodeFromRepository = ContentTreeSectionNodeRepository.GetAllContentTreeSectionNodes().Where(a => a.TreeNodeId == contentTreeSectionInputModel.TreeNodeId).FirstOrDefault();
			ContentTreeSectionInputModelToContentTreeSectionNodeMapper.LoadIntoInstance(contentTreeSectionInputModel, contentTreeSectionNodeFromRepository);
			ContentTreeSectionNodeRepository.Update(contentTreeSectionNodeFromRepository);
			
			if (contentTreeSectionInputModel.Action != null)
			{
				if (contentTreeSectionInputModel.Action.ToLower() == "save and exit")
					return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTree"},
													{"action", "Index"},
			                                 	});
			}

			return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTreeSectionNode"},
													{"action", "Modify"},
													{ "treeNodeId", contentTreeSectionInputModel == null ? "0" : contentTreeSectionInputModel.TreeNodeId },
			                                 	});
		}

		public ActionResult Modify(string treeNodeId)
		{
			var viewData = ViewData;

			var contentTreeSection = ContentTreeSectionNodeRepository.GetAllContentTreeSectionNodes().Where(a => a.TreeNodeId == treeNodeId).FirstOrDefault();
			var contentTreeSectionInputModel = contentTreeSection == null ? new ContentTreeSectionInputModel()
			                		                        	{
			                		                        		TreeNodeId = treeNodeId,
			                		                        	} 
										: contentTreeSectionNodeToContentTreeSectionInputModelMapper.CreateInstance(contentTreeSection);

			var viewModel = new ContentTreeSectionNodeViewModel()
			                	{
									Action = "Modify",
			                		ContentTreeSectionInputModel = contentTreeSectionInputModel,
			                	};

			return View("Modify", viewModel);
		}

		private string GetRedirectUrlToModifyMethod(ContentTreeSectionInputModel ContentTreeSectionInputModel)
		{
			if (Url == null) return "/";
			return Url.Action("Modify", "ContentTreeSectionNode", new { treeNodeId = ContentTreeSectionInputModel == null ? "0" : ContentTreeSectionInputModel.TreeNodeId });
		}
	}
}
