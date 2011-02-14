using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bennington.ContentTree.Helpers;
using Bennington.ContentTree.Providers.SectionNodeProvider.Repositories;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Controllers
{
	public class ContentTreeSectionController : Controller
	{
		private readonly ITreeNodeIdToUrl treeNodeIdToUrl;
		private readonly IContentTreeSectionNodeRepository contentTreeSectionNodeRepository;
		private readonly IUrlToTreeNodeSummaryMapper urlToTreeNodeSummaryMapper;

		public ContentTreeSectionController(ITreeNodeIdToUrl treeNodeIdToUrl, 
											IContentTreeSectionNodeRepository contentTreeSectionNodeRepository,
											IUrlToTreeNodeSummaryMapper	urlToTreeNodeSummaryMapper)
		{
			this.urlToTreeNodeSummaryMapper = urlToTreeNodeSummaryMapper;
			this.contentTreeSectionNodeRepository = contentTreeSectionNodeRepository;
			this.treeNodeIdToUrl = treeNodeIdToUrl;
		}

		public ActionResult Index()
		{
			var treeNodeSummary = urlToTreeNodeSummaryMapper.CreateInstance(System.Web.HttpContext.Current.Request.RawUrl);
			var sectionNode = contentTreeSectionNodeRepository.GetAllContentTreeSectionNodes().Where(a => a.TreeNodeId == treeNodeSummary.Id).FirstOrDefault();
			if (sectionNode == null) return null;
			
			var url = treeNodeIdToUrl.GetUrlByTreeNodeId(sectionNode.DefaultTreeNodeId);

			return new RedirectResult(url);
			//return new TransferResult(url);
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

			httpContext.RewritePath(Url, true);

			IHttpHandler httpHandler = new MvcHttpHandler();
			httpHandler.ProcessRequest(HttpContext.Current);
		}
	}

}
