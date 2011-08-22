using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Domain.Commands;
using Bennington.ContentTree.Providers.ContentNodeProvider.Context;
using Bennington.ContentTree.Providers.ContentNodeProvider.Helpers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Bennington.ContentTree.Providers.ContentNodeProvider.ViewModelBuilders;
using Bennington.ContentTree.Providers.ContentNodeProvider.ViewModelBuilders.Helpers;
using Bennington.ContentTree.Repositories;
using Bennington.Core.Helpers;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Controllers
{
	public class ContentTreeNodeController : Controller
	{
		private readonly IContentTreeNodeVersionContext contentTreeNodeVersionContext;
		private readonly IContentTreeNodeToContentTreeNodeInputModelMapper contentTreeNodeToContentTreeNodeInputModelMapper;
		private readonly IContentTreeNodeContext contentTreeNodeContext;
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly ITreeNodeProviderContext treeNodeProviderContext;
		private readonly IContentTreeNodeDisplayViewModelBuilder contentTreeNodeDisplayViewModelBuilder;
		private readonly IRawUrlGetter rawUrlGetter;
		private readonly ICommandBus commandBus;
		private readonly IGuidGetter guidGetter;
		private readonly IContentTreeNodeFileUploadPersister contentTreeNodeFileUploadPersister;

		public ContentTreeNodeController(IContentTreeNodeVersionContext contentTreeNodeVersionContext, 
											IContentTreeNodeToContentTreeNodeInputModelMapper contentTreeNodeToContentTreeNodeInputModelMapper, 
											IContentTreeNodeInputModelToContentTreeNodeMapper contentTreeNodeInputModelToContentTreeNodeMapper, 
											IContentTreeNodeContext contentTreeNodeContext, 
											ITreeNodeRepository treeNodeRepository, 
											ITreeNodeProviderContext treeNodeProviderContext,  
											IContentTreeNodeDisplayViewModelBuilder contentTreeNodeDisplayViewModelBuilder, 
											IRawUrlGetter rawUrlGetter,
											ICommandBus commandBus,
											IGuidGetter guidGetter,
											IContentTreeNodeFileUploadPersister contentTreeNodeFileUploadPersister)
		{
			this.contentTreeNodeFileUploadPersister = contentTreeNodeFileUploadPersister;
			this.guidGetter = guidGetter;
			this.commandBus = commandBus;
			this.rawUrlGetter = rawUrlGetter;
			this.contentTreeNodeDisplayViewModelBuilder = contentTreeNodeDisplayViewModelBuilder;
			this.treeNodeProviderContext = treeNodeProviderContext;
			this.treeNodeRepository = treeNodeRepository;
			this.contentTreeNodeContext = contentTreeNodeContext;
			this.contentTreeNodeToContentTreeNodeInputModelMapper = contentTreeNodeToContentTreeNodeInputModelMapper;
			this.contentTreeNodeVersionContext = contentTreeNodeVersionContext;
		}

		public virtual ActionResult Index()
		{
			return View("Index", contentTreeNodeDisplayViewModelBuilder.BuildViewModel(rawUrlGetter.GetRawUrl(), (ControllerContext ?? new ControllerContext()).RouteData));
		}

		[Authorize]
		public virtual ActionResult Delete(string treeNodeId)
		{
			var treeNodeGuidId = new Guid(treeNodeId);
			commandBus.Send(new DeleteTreeNodeCommand()
			                	{
									AggregateRootId = treeNodeGuidId
			                	});
			commandBus.Send(new DeletePageCommand()
			                	{
									TreeNodeId = treeNodeGuidId,
									AggregateRootId = treeNodeGuidId,
			                	});
			return new RedirectToRouteResult(new RouteValueDictionary { { "controller", "TreeManager" }, { "action", "Index" }});
		}
		
		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		public virtual ActionResult Create(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			if (string.IsNullOrEmpty(contentTreeNodeInputModel.Action)) contentTreeNodeInputModel.Action = "Index";
			if (ModelState.IsValid == false)
				return View("Modify", new ModifyViewModel()
				                      	{
				                      		ContentTreeNodeInputModel = contentTreeNodeInputModel,
											Action = "Create",
				                      	});

			var treeNodeId = contentTreeNodeContext.CreateTreeNodeAndReturnTreeNodeId(contentTreeNodeInputModel);
			contentTreeNodeFileUploadPersister.SaveFilesByTreeNodeIdAndAction(treeNodeId, contentTreeNodeInputModel.Action);
			
			SetAppropriateInputModelPropertiesFromFilesCollection(contentTreeNodeInputModel);

			commandBus.Send(new CreatePageCommand()
			                	{
									PageId = guidGetter.GetGuid(),
									TreeNodeId = new Guid(treeNodeId),
			                		Body = contentTreeNodeInputModel.Body,
									HeaderText = contentTreeNodeInputModel.HeaderText,
									HeaderImage = contentTreeNodeInputModel.HeaderImage,
									Name = contentTreeNodeInputModel.Name,
									Sequence = contentTreeNodeInputModel.Sequence,
									UrlSegment = contentTreeNodeInputModel.UrlSegment,
									Type = Type.GetType(contentTreeNodeInputModel.Type),
									Inactive = contentTreeNodeInputModel.Inactive,
									Hidden = contentTreeNodeInputModel.Hidden,
			                	});

			if (!string.IsNullOrEmpty(contentTreeNodeInputModel.FormAction))
			{
				if (contentTreeNodeInputModel.FormAction.ToLower() == "save and exit")
					return new RedirectToRouteResult(new RouteValueDictionary()
			                                 	{
			                                 		{"controller", "ContentTree"},
													{"action", "Index"},
			                                 	});
			}
			return new RedirectResult(GetRedirectUrlToModifyMethod(contentTreeNodeInputModel));
		}

		private void SetAppropriateInputModelPropertiesFromFilesCollection(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			if (HttpContext == null) return;

			if (HttpContext.Request.Files.AllKeys.Where(a => a == "ContentTreeNodeInputModel_HeaderImage").Any())
			{
				contentTreeNodeInputModel.HeaderImage = HttpContext.Request.Files["ContentTreeNodeInputModel_HeaderImage"].FileName;
			}
		}


		[Authorize]
		public virtual ActionResult Create(string parentTreeNodeId, string providerType)
		{
			return View("Modify", new ModifyViewModel()
			                      	{
										Action = "Create",
			                      		ContentTreeNodeInputModel = new ContentTreeNodeInputModel()
			                      		                        	{
			                      		                        		ParentTreeNodeId = parentTreeNodeId,
																		Type = providerType,
			                      		                        	}
			                      	});
		}

		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		public virtual ActionResult Modify(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			SetAppropriateInputModelPropertiesFromFilesCollection(contentTreeNodeInputModel);
			contentTreeNodeFileUploadPersister.SaveFilesByTreeNodeIdAndAction(contentTreeNodeInputModel.TreeNodeId, contentTreeNodeInputModel.Action);
			if (ModelState.IsValid == false)
				return View("Modify", new ModifyViewModel() { Action = "Modify", ContentTreeNodeInputModel = contentTreeNodeInputModel });

			var contentTreeNode = contentTreeNodeContext.GetContentTreeNodesByTreeId(contentTreeNodeInputModel.TreeNodeId).Where(a => a.Action == contentTreeNodeInputModel.Action).FirstOrDefault();
			if (contentTreeNode != null)
			{
				var modifyPageComand = new ModifyPageCommand()
				{
					AggregateRootId = new Guid(contentTreeNodeInputModel.PageId),
					TreeNodeId = new Guid(contentTreeNodeInputModel.TreeNodeId),
					HeaderText = contentTreeNodeInputModel.HeaderText,
					HeaderImage = contentTreeNodeInputModel.RemoveHeaderImage ? null : (string.IsNullOrEmpty(contentTreeNodeInputModel.HeaderImage) ? contentTreeNode.HeaderImage : contentTreeNodeInputModel.HeaderImage),
					Name = contentTreeNodeInputModel.Name,
					Body = contentTreeNodeInputModel.Body,
					ParentId = contentTreeNodeInputModel.ParentTreeNodeId,
					Sequence = contentTreeNodeInputModel.Sequence,
					UrlSegment = contentTreeNodeInputModel.UrlSegment,
					ActionId = contentTreeNodeInputModel.Action,
					Hidden = contentTreeNodeInputModel.Hidden,
					Inactive = contentTreeNodeInputModel.Inactive
				};
				commandBus.Send(modifyPageComand);
			} else {
				commandBus.Send(new CreatePageCommand()
				                	{
				                		TreeNodeId = new Guid(contentTreeNodeInputModel.TreeNodeId),
										PageId = guidGetter.GetGuid(),
										Body = contentTreeNodeInputModel.Body,
										HeaderText = contentTreeNodeInputModel.HeaderText,
										HeaderImage = contentTreeNodeInputModel.HeaderImage,
										Name = contentTreeNodeInputModel.Name,
										UrlSegment = contentTreeNodeInputModel.UrlSegment,
										Action = contentTreeNodeInputModel.Action,
										Hidden = contentTreeNodeInputModel.Hidden,
										Inactive = contentTreeNodeInputModel.Inactive,
				                	});
			}

			if ((!string.IsNullOrEmpty(contentTreeNodeInputModel.FormAction)) && (contentTreeNodeInputModel.FormAction.StartsWith("Publish")))
				commandBus.Send(new PublishPageCommand()
				{
					PageId = new Guid(contentTreeNodeInputModel.PageId)
				});

			if (!string.IsNullOrEmpty(contentTreeNodeInputModel.FormAction))
			{
				if (contentTreeNodeInputModel.FormAction.ToLower() == "save and exit")
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
													{ "contentItemId", contentTreeNodeInputModel == null ? "Index" : contentTreeNodeInputModel.Action },
			                                 	});
		}

		[Authorize]
		public virtual ActionResult Modify(string treeNodeId, string contentItemId)
		{
			if (string.IsNullOrEmpty(contentItemId)) contentItemId = "Index";

            var all = contentTreeNodeVersionContext.GetAllContentTreeNodes().ToArray();
		    var test = contentTreeNodeVersionContext.GetAllContentTreeNodes().Where(a => a.TreeNodeId == treeNodeId).ToArray();
		    

			var contentTreeNode = contentTreeNodeVersionContext.GetAllContentTreeNodes().Where(a => a.TreeNodeId == treeNodeId && a.Action == contentItemId).FirstOrDefault();
			var contentTreeNodeInputModel = contentTreeNode == null ? new ContentTreeNodeInputModel()
			                		                        	{
			                		                        		TreeNodeId = treeNodeId,
																	Action = contentItemId,
			                		                        	} 
										: contentTreeNodeToContentTreeNodeInputModelMapper.CreateInstance(contentTreeNode);

			var viewModel = new ModifyViewModel()
			                	{
									Action = "Modify",
			                		ContentTreeNodeInputModel = contentTreeNodeInputModel,
			                	};
			if (string.IsNullOrEmpty(viewModel.ContentTreeNodeInputModel.Action))
				viewModel.ContentTreeNodeInputModel.Action = "Index";

			return View("Modify", viewModel);
		}

        public ActionResult Display()
        {
            return View();
        }

		[Authorize]
		public virtual ActionResult ContentItemNavigation(string treeNodeId)
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
