using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Paragon.ContentTreeSectionNodeProvider.Context;
using Paragon.ContentTreeSectionNodeProvider.Mappers;
using Paragon.ContentTreeSectionNodeProvider.Models;
using Paragon.ContentTreeSectionNodeProvider.Repositories;
using Paragon.Pages.Routing.Helpers;

namespace Paragon.ContentTreeSectionNodeProvider.Controllers
{
	public class ContentTreeSectionController : Controller
	{
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
		private IContentTreeSectionNodeRepository contentTreeSectionNodeRepository;

		public ContentTreeSectionController(ITreeNodeIdToUrl treeNodeIdToUrl, IContentTreeSectionNodeRepository contentTreeSectionNodeRepository)
		{
			this.contentTreeSectionNodeRepository = contentTreeSectionNodeRepository;
			this.treeNodeIdToUrl = treeNodeIdToUrl;
		}

		public ActionResult Index(string treeNodeId)
		{
			var sectionNode = contentTreeSectionNodeRepository.GetAllContentTreeSectionNodes().Where(a => a.TreeNodeId == treeNodeId).FirstOrDefault();
			if (sectionNode == null) return null;
			
			var url = treeNodeIdToUrl.GetUrlByTreeNodeId(sectionNode.DefaultTreeNodeId);

			return new TransferResult(url);
		}
	}

	public class TransferResult : RedirectResult
	{
		public TransferResult(string url)
			: base(url)
		{
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var httpContext = HttpContext.Current;

			httpContext.RewritePath(Url, false);

			IHttpHandler httpHandler = new MvcHttpHandler();
			httpHandler.ProcessRequest(HttpContext.Current);
		}
	}

}
