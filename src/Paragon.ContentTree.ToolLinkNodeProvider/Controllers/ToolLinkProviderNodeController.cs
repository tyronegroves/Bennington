using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paragon.ContentTree.ToolLinkNodeProvider.Models;
using Paragon.ContentTree.ToolLinkNodeProvider.ViewModelBuilders;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Controllers
{
    public class ToolLinkProviderNodeController : Controller
    {
    	private readonly IModifyViewModelBuilder modifyViewModelBuilder;

    	public ToolLinkProviderNodeController(IModifyViewModelBuilder modifyViewModelBuilder)
    	{
    		this.modifyViewModelBuilder = modifyViewModelBuilder;
    	}

		public ActionResult Create(string parentTreeNodeId)
		{
			return View("Modify", modifyViewModelBuilder.BuildViewModel(new ToolLinkInputModel()
			                                                            	{
																				Action = "Create",
			                                                            		ParentTreeNodeId = parentTreeNodeId,
			                                                            	}));
		}

		[HttpPost]
		public ActionResult Create(ToolLinkInputModel toolLinkInputModel)
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
