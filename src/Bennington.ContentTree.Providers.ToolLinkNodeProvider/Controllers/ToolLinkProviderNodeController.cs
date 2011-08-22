using System.Web.Mvc;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models;
using Bennington.ContentTree.Providers.ToolLinkNodeProvider.ViewModelBuilders;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Controllers
{
    public class ToolLinkProviderNodeController : Controller
    {
    	private readonly IModifyViewModelBuilder modifyViewModelBuilder;

    	public ToolLinkProviderNodeController(IModifyViewModelBuilder modifyViewModelBuilder)
    	{
    		this.modifyViewModelBuilder = modifyViewModelBuilder;
    	}

        [Authorize]
		public ActionResult Create(string parentTreeNodeId)
		{
			return View("Modify", modifyViewModelBuilder.BuildViewModel(new ToolLinkInputModel()
			                                                            	{
																				Action = "Create",
			                                                            		ParentTreeNodeId = parentTreeNodeId,
			                                                            	}));
		}

		[HttpPost]
        [Authorize]
		public ActionResult Create(ToolLinkInputModel toolLinkInputModel)
		{
			if (!ModelState.IsValid)
				return View("Modify", modifyViewModelBuilder.BuildViewModel(toolLinkInputModel));

			return new RedirectResult(GetRedirectUrlToModifyMethod(toolLinkInputModel));
		}

        [Authorize]
		public ActionResult Modify(string treeNodeId)
		{
			return View("Modify", modifyViewModelBuilder.BuildViewModel(new ToolLinkInputModel()
			                                                            	{
																				Action = "Modify",
			                                                            		TreeNodeId = treeNodeId,
			                                                            	}));
		}

		[HttpPost]
        [Authorize]
		public ActionResult Modify(ToolLinkInputModel toolLinkInputModel)
		{
			if (!ModelState.IsValid)
				return View("Modify", modifyViewModelBuilder.BuildViewModel(toolLinkInputModel));

			return new RedirectResult(GetRedirectUrlToModifyMethod(toolLinkInputModel));
		}

		private string GetRedirectUrlToModifyMethod(ToolLinkInputModel toolLinkInputModel)
		{
			if (Url == null) return "/";
			return Url.Action("Modify", "ToolLinkProviderNode", new { treeNodeId = toolLinkInputModel == null ? "0" : toolLinkInputModel.TreeNodeId });
		}
    }
}
