using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Paragon.ContentTree.ExampleEngineNodeProvider.Context;
using Paragon.ContentTree.ExampleEngineNodeProvider.Mappers;
using Paragon.ContentTree.ExampleEngineNodeProvider.Models;
using Paragon.ContentTree.ExampleEngineNodeProvider.Repositories;

namespace Paragon.ContentTree.ExampleEngineNodeProvider.Controllers
{
	public class ContentTreeContactUsNodeController : Controller
	{
		private readonly IContentTreeContactUsNodeRepository contentTreeContactUsNodeRepository;
		private readonly IContentTreeContactUsNodeToContentTreeContactUsInputModelMapper contentTreeContactUsNodeToContentTreeContactUsInputModelMapper;
		private readonly IContentTreeContactUsInputModelToContentTreeContactUsNodeMapper contentTreeContactUsInputModelToContentTreeContactUsNodeMapper;
		private readonly IContentTreeContactUsNodeContext contentTreeContactUsNodeContext;

		public ContentTreeContactUsNodeController(IContentTreeContactUsNodeRepository contentTreeContactUsNodeRepository, IContentTreeContactUsNodeToContentTreeContactUsInputModelMapper ContentTreeContactUsNodeToContentTreeContactUsInputModelMapper, IContentTreeContactUsInputModelToContentTreeContactUsNodeMapper ContentTreeContactUsInputModelToContentTreeContactUsNodeMapper, IContentTreeContactUsNodeContext ContentTreeContactUsNodeContext)
		{
			this.contentTreeContactUsNodeContext = ContentTreeContactUsNodeContext;
			this.contentTreeContactUsInputModelToContentTreeContactUsNodeMapper = ContentTreeContactUsInputModelToContentTreeContactUsNodeMapper;
			this.contentTreeContactUsNodeToContentTreeContactUsInputModelMapper = ContentTreeContactUsNodeToContentTreeContactUsInputModelMapper;
			this.contentTreeContactUsNodeRepository = contentTreeContactUsNodeRepository;
		}

		public ActionResult Delete(string treeNodeId)
		{
			contentTreeContactUsNodeContext.Delete(treeNodeId);
			return new RedirectToRouteResult(new RouteValueDictionary { { "controller", "ContentTree" }, { "action", "Index" } });
		}

		[HttpPost]
		public ActionResult Create(ContentTreeContactUsNodeInputModel contentTreeContactUsInputModel)
		{
			if (ModelState.IsValid == false)
			{
				contentTreeContactUsInputModel.Action = "Create";
				return View("Modify", contentTreeContactUsInputModel);
			}
				

			contentTreeContactUsInputModel.TreeNodeId = contentTreeContactUsNodeContext.CreateTreeNodeAndReturnTreeNodeId(contentTreeContactUsInputModel);

			if (contentTreeContactUsInputModel.Action.ToLower() == "save and exit")
				return new RedirectToRouteResult(new RouteValueDictionary()
			                                    {
			                                        {"controller", "ContentTree"},
			                                        {"action", "Index"},
			                                    });

			return new RedirectResult(GetRedirectUrlToModifyMethod(contentTreeContactUsInputModel));
		}

		public ActionResult Create(string parentTreeNodeId)
		{
			return View("Modify", new ContentTreeContactUsNodeInputModel()
									{
										Action = "Create",
										ParentTreeNodeId = parentTreeNodeId
									});
		}

		[HttpPost]
		public ActionResult Modify(ContentTreeContactUsNodeInputModel contentTreeContactUsInputModel)
		{
			if (ModelState.IsValid == false)
			{
				contentTreeContactUsInputModel.Action = "Modify";
				return View("Modify", contentTreeContactUsInputModel);
			}
			
			var contentTreeContactUsNodeFromRepository = contentTreeContactUsNodeRepository.GetAll().Where(a => a.TreeNodeId == contentTreeContactUsInputModel.TreeNodeId).FirstOrDefault();
			contentTreeContactUsInputModelToContentTreeContactUsNodeMapper.LoadIntoInstance(contentTreeContactUsInputModel, contentTreeContactUsNodeFromRepository);
			contentTreeContactUsNodeRepository.Update(contentTreeContactUsNodeFromRepository);

			if (contentTreeContactUsInputModel.Action != null)
			{
				if (contentTreeContactUsInputModel.Action.ToLower() == "save and exit")
					return new RedirectToRouteResult(new RouteValueDictionary()
			                                    {
			                                        {"controller", "ContentTree"},
			                                        {"action", "Index"},
			                                    });
			}

			return new RedirectToRouteResult(new RouteValueDictionary()
			                                    {
			                                        {"controller", "ContentTreeContactUsNode"},
			                                        {"action", "Modify"},
			                                        { "treeNodeId", contentTreeContactUsInputModel == null ? "0" : contentTreeContactUsInputModel.TreeNodeId },
			                                    });
		}

		public ActionResult Modify(string treeNodeId)
		{
			var contentTreeContactUs = contentTreeContactUsNodeRepository.GetAll().Where(a => a.TreeNodeId == treeNodeId).FirstOrDefault();
			var contentTreeContactUsInputModel = contentTreeContactUs == null ? new ContentTreeContactUsNodeInputModel()
																{
																	TreeNodeId = treeNodeId,
																}
										: contentTreeContactUsNodeToContentTreeContactUsInputModelMapper.CreateInstance(contentTreeContactUs);

			contentTreeContactUsInputModel.Action = "Modify";
			return View("Modify", contentTreeContactUsInputModel);
		}

		private string GetRedirectUrlToModifyMethod(ContentTreeContactUsNodeInputModel contentTreeContactUsInputModel)
		{
			if (Url == null) return "/";
			return Url.Action("Modify", "ContentTreeContactUsNode", new { treeNodeId = contentTreeContactUsInputModel == null ? "0" : contentTreeContactUsInputModel.TreeNodeId });
		}
	}
}
