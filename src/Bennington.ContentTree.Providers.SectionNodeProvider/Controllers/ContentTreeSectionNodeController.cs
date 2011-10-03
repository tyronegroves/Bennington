using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Domain.Commands;
using Bennington.ContentTree.Providers.SectionNodeProvider.Context;
using Bennington.ContentTree.Providers.SectionNodeProvider.Mappers;
using Bennington.ContentTree.Providers.SectionNodeProvider.Models;
using Bennington.ContentTree.Providers.SectionNodeProvider.Repositories;
using Bennington.Core.Helpers;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Controllers
{
	public class ContentTreeSectionNodeController : Controller
	{
		private readonly IContentTreeSectionNodeRepository contentTreeSectionNodeRepository;
		private readonly IContentTreeSectionNodeToContentTreeSectionInputModelMapper contentTreeSectionNodeToContentTreeSectionInputModelMapper;
		private readonly ICommandBus commandBus;
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;
		private readonly IGuidGetter guidGetter;

		public ContentTreeSectionNodeController(IContentTreeSectionNodeRepository contentTreeSectionNodeRepository, 
												IContentTreeSectionNodeToContentTreeSectionInputModelMapper contentTreeSectionNodeToContentTreeSectionInputModelMapper, 
												IContentTreeSectionInputModelToContentTreeSectionNodeMapper contentTreeSectionInputModelToContentTreeSectionNodeMapper, 
												IContentTreeSectionNodeContext contentTreeSectionNodeContext,
												ICommandBus commandBus,
												ITreeNodeSummaryContext treeNodeSummaryContext,
												IGuidGetter guidGetter)
		{
			this.guidGetter = guidGetter;
			this.treeNodeSummaryContext = treeNodeSummaryContext;
			this.commandBus = commandBus;
			this.contentTreeSectionNodeToContentTreeSectionInputModelMapper = contentTreeSectionNodeToContentTreeSectionInputModelMapper;
			this.contentTreeSectionNodeRepository = contentTreeSectionNodeRepository;
		}

		[Authorize]
		public ActionResult Delete(string id)
		{
			commandBus.Send(new DeleteTreeNodeCommand()
			{
				AggregateRootId = new Guid(id)
			});
			commandBus.Send(new DeleteSectionCommand(){ AggregateRootId = new Guid(id) });
		    
            return new RedirectResult("/ContentTree");
            //var routeValueDictionary = new RouteValueDictionary();
            //routeValueDictionary.Add("controller", "ContentTree");
            //routeValueDictionary.Add("action", "Index");
            //return new RedirectToRouteResult(routeValueDictionary);
		}

		[Authorize]
		[HttpPost]
		public ActionResult Create(ContentTreeSectionInputModel contentTreeSectionInputModel)
		{
			if (ModelState.IsValid == false)
				return View("Modify", new ContentTreeSectionNodeViewModel()
				                      	{
				                      		ContentTreeSectionInputModel = contentTreeSectionInputModel,
											Action = "Create",
				                      	});

			var treeNodeIdString = treeNodeSummaryContext.Create(contentTreeSectionInputModel.ParentTreeNodeId, typeof(SectionNodeProvider).AssemblyQualifiedName);
			
			commandBus.Send(new CreateSectionCommand()
			                	{
									SectionId = guidGetter.GetGuid().ToString(),
									TreeNodeId = treeNodeIdString,
			                		DefaultTreeNodeId = contentTreeSectionInputModel.DefaultTreeNodeId,
									Name = contentTreeSectionInputModel.Name,
									ParentTreeNodeId = contentTreeSectionInputModel.ParentTreeNodeId,
									Sequence = contentTreeSectionInputModel.Sequence,
									UrlSegment = contentTreeSectionInputModel.UrlSegment,
									Hidden = contentTreeSectionInputModel.Hidden,
									Inactive = contentTreeSectionInputModel.Inactive,
			                	});

			if (contentTreeSectionInputModel.Action.ToLower() == "save and exit")
				return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTree"},
													{"action", "Index"},
			                                 	});

			contentTreeSectionInputModel.TreeNodeId = treeNodeIdString;
			return new RedirectResult(GetRedirectUrlToModifyMethod(contentTreeSectionInputModel));
		}

		[Authorize]
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

		[Authorize]
		[HttpPost]
		public ActionResult Modify(ContentTreeSectionInputModel contentTreeSectionInputModel)
		{
			if (ModelState.IsValid == false)
				return View("Modify", new ContentTreeSectionNodeViewModel() { Action = "Modify", ContentTreeSectionInputModel = contentTreeSectionInputModel });

			commandBus.Send(new ModifySectionCommand()
			                	{
									AggregateRootId = new Guid(contentTreeSectionInputModel.SectionId),
									SectionId = contentTreeSectionInputModel.SectionId,
									TreeNodeId = contentTreeSectionInputModel.TreeNodeId,
			                		DefaultTreeNodeId = contentTreeSectionInputModel.DefaultTreeNodeId,
									ParentTreeNodeId = contentTreeSectionInputModel.ParentTreeNodeId,
									UrlSegment = contentTreeSectionInputModel.UrlSegment,
									Sequence = contentTreeSectionInputModel.Sequence,
									Name = contentTreeSectionInputModel.Name,
									Hidden = contentTreeSectionInputModel.Hidden,
									Inactive = contentTreeSectionInputModel.Inactive,
			                	});

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
													{ "id", contentTreeSectionInputModel == null ? "0" : contentTreeSectionInputModel.TreeNodeId },
			                                 	});
		}

		[Authorize]
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

		private string GetRedirectUrlToModifyMethod(ContentTreeSectionInputModel contentTreeSectionInputModel)
		{
			if (Url == null) return "/";
			return Url.Action("Modify", "ContentTreeSectionNode", new { treeNodeId = contentTreeSectionInputModel == null ? "0" : contentTreeSectionInputModel.TreeNodeId });
		}
	}
}
