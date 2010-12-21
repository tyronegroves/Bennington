using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Paragon.ContentTree.SectionNodeProvider.Context;
using Paragon.ContentTree.SectionNodeProvider.Mappers;
using Paragon.ContentTree.SectionNodeProvider.Models;
using Paragon.ContentTree.SectionNodeProvider.Repositories;

namespace Paragon.ContentTree.SectionNodeProvider.Controllers
{
	public class ContentTreeSectionNodeController : Controller
	{
		private readonly IContentTreeSectionNodeRepository contentTreeSectionNodeRepository;
		private readonly IContentTreeSectionNodeToContentTreeSectionInputModelMapper contentTreeSectionNodeToContentTreeSectionInputModelMapper;
		private readonly IContentTreeSectionInputModelToContentTreeSectionNodeMapper contentTreeSectionInputModelToContentTreeSectionNodeMapper;
		private readonly IContentTreeSectionNodeContext contentTreeSectionNodeContext;

		public ContentTreeSectionNodeController(IContentTreeSectionNodeRepository contentTreeSectionNodeRepository, IContentTreeSectionNodeToContentTreeSectionInputModelMapper contentTreeSectionNodeToContentTreeSectionInputModelMapper, IContentTreeSectionInputModelToContentTreeSectionNodeMapper contentTreeSectionInputModelToContentTreeSectionNodeMapper, IContentTreeSectionNodeContext ContentTreeSectionNodeContext)
		{
			this.contentTreeSectionNodeContext = ContentTreeSectionNodeContext;
			this.contentTreeSectionInputModelToContentTreeSectionNodeMapper = contentTreeSectionInputModelToContentTreeSectionNodeMapper;
			this.contentTreeSectionNodeToContentTreeSectionInputModelMapper = contentTreeSectionNodeToContentTreeSectionInputModelMapper;
			this.contentTreeSectionNodeRepository = contentTreeSectionNodeRepository;
		}

		public ActionResult Delete(string treeNodeId)
		{
			contentTreeSectionNodeContext.Delete(treeNodeId);
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
			
			contentTreeSectionInputModel.TreeNodeId = contentTreeSectionNodeContext.CreateTreeNodeAndReturnTreeNodeId(contentTreeSectionInputModel);

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

			var contentTreeSectionNodeFromRepository = contentTreeSectionNodeRepository.GetAllContentTreeSectionNodes().Where(a => a.TreeNodeId == contentTreeSectionInputModel.TreeNodeId).FirstOrDefault();
			contentTreeSectionInputModelToContentTreeSectionNodeMapper.LoadIntoInstance(contentTreeSectionInputModel, contentTreeSectionNodeFromRepository);
			contentTreeSectionNodeRepository.Update(contentTreeSectionNodeFromRepository);
			
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

			var contentTreeSection = contentTreeSectionNodeRepository.GetAllContentTreeSectionNodes().Where(a => a.TreeNodeId == treeNodeId).FirstOrDefault();
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
