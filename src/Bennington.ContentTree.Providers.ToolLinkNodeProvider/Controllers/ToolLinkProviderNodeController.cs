using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Domain.Commands;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Data;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Repositories;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.ViewModelBuilders;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Controllers
{
    public class ToolLinkProviderNodeController : Controller
    {
    	private readonly IModifyViewModelBuilder modifyViewModelBuilder;
        private readonly IToolLinkProviderDraftRepository toolLinkProviderDraftRepository;
        private readonly ITreeNodeSummaryContext treeNodeSummaryContext;
        private readonly ICommandBus commandBus;
        private readonly ICurrentUserContext currentUserContext;

        public ToolLinkProviderNodeController(IModifyViewModelBuilder modifyViewModelBuilder,
                                                IToolLinkProviderDraftRepository toolLinkProviderDraftRepository,
                                                ITreeNodeSummaryContext treeNodeSummaryContext,
                                                ICommandBus commandBus,
                                                ICurrentUserContext currentUserContext)
        {
            this.currentUserContext = currentUserContext;
            this.commandBus = commandBus;
            this.treeNodeSummaryContext = treeNodeSummaryContext;
            this.toolLinkProviderDraftRepository = toolLinkProviderDraftRepository;
            this.modifyViewModelBuilder = modifyViewModelBuilder;
        }

        [Authorize]
		public ActionResult Create(string parentTreeNodeId)
		{
			return View("Modify", modifyViewModelBuilder.BuildViewModel(new ToolLinkInputModel()
			                                                            	{
			                                                            		ParentTreeNodeId = parentTreeNodeId,
			                                                            	}));
		}

		[HttpPost]
        [Authorize]
		public ActionResult Create(ToolLinkInputModel toolLinkInputModel)
		{
			if (!ModelState.IsValid)
				return View("Modify", modifyViewModelBuilder.BuildViewModel(toolLinkInputModel));

            var newTreeNodeId = treeNodeSummaryContext.Create(toolLinkInputModel.ParentTreeNodeId, typeof(ToolLinkNodeProvider).AssemblyQualifiedName);
		    
            var toolLinkProviderDraft = new ToolLinkProviderDraft()
		                {
		                    Id = newTreeNodeId,
		                    Hidden = toolLinkInputModel.Hidden,
		                    Name = toolLinkInputModel.Name,
		                    Inactive = toolLinkInputModel.Inactive,
		                    Url = toolLinkInputModel.Url,
		                    UrlSegment = toolLinkInputModel.UrlSegment,
                            Sequence = toolLinkInputModel.Sequence,
                            LastModifyDate = DateTime.Now,
                            LastModifyBy = currentUserContext.GetCurrentPrincipal().Identity.Name
		                };
		    toolLinkProviderDraftRepository.SaveAndReturnId(toolLinkProviderDraft);

            return new RedirectResult(GetRedirectUrlToModifyMethod(toolLinkProviderDraft));
		}

        [Authorize]
		public ActionResult Modify(string treeNodeId)
        {
            var toolLinkProviderDraft = toolLinkProviderDraftRepository.GetAll().Where(a => a.Id == treeNodeId).FirstOrDefault();

			return View("Modify", modifyViewModelBuilder.BuildViewModel(new ToolLinkInputModel()
			                                                            	{
			                                                            		TreeNodeId = treeNodeId,
                                                                                Hidden = toolLinkProviderDraft.Hidden,
                                                                                Inactive = toolLinkProviderDraft.Inactive,
                                                                                Name = toolLinkProviderDraft.Name,
                                                                                Url = toolLinkProviderDraft.Url,
                                                                                UrlSegment = toolLinkProviderDraft.UrlSegment,
                                                                                Sequence = toolLinkProviderDraft.Sequence,
			                                                            	}));
		}

		[HttpPost]
        [Authorize]
		public ActionResult Modify(ToolLinkInputModel toolLinkInputModel)
		{
			if (!ModelState.IsValid)
				return View("Modify", modifyViewModelBuilder.BuildViewModel(toolLinkInputModel));

		    var toolLinkProviderDraft = new ToolLinkProviderDraft()
		                                    {
		                                        Hidden = toolLinkInputModel.Hidden,
		                                        Inactive = toolLinkInputModel.Inactive,
		                                        Name = toolLinkInputModel.Name,
		                                        Url = toolLinkInputModel.Url,
		                                        UrlSegment = toolLinkInputModel.UrlSegment,
		                                        Id = toolLinkInputModel.TreeNodeId,
		                                        Sequence = toolLinkInputModel.Sequence,
                                                LastModifyDate = DateTime.Now,
                                                LastModifyBy = currentUserContext.GetCurrentPrincipal().Identity.Name
		                                    };

		    toolLinkProviderDraftRepository.SaveAndReturnId(toolLinkProviderDraft);

            return new RedirectResult(GetRedirectUrlToModifyMethod(toolLinkProviderDraft));
		}

        public ActionResult Delete(string treeNodeId)
        {
            commandBus.Send(new DeleteTreeNodeCommand()
            {
                AggregateRootId = new Guid(treeNodeId)
            });

            var routes = new RouteValueDictionary();
            routes.Add("controller", "TreeManager");
            routes.Add("action", "Index");
            return new RedirectToRouteResult(routes);
        }

		private string GetRedirectUrlToModifyMethod(ToolLinkProviderDraft toolLinkProviderDraft)
		{
			if (Url == null) return "/";
			return Url.Action("Modify", "ToolLinkProviderNode", new { treeNodeId = toolLinkProviderDraft.Id });
		}
    }
}
