using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paragon.ContentTree.SectionNodeProvider.Repositories;
using Paragon.Pages.Routing.Helpers;

namespace Paragon.ContentTree.SectionNodeProvider.Controllers
{
	public class ContentTreeSectionController : Controller
	{
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
		private readonly IContentTreeSectionNodeRepository contentTreeSectionNodeRepository;

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
