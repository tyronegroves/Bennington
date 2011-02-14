using System.Linq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Models;

namespace Bennington.ContentTree.Helpers
{
	public interface IUrlToTreeNodeSummaryMapper
	{
		TreeNodeSummary CreateInstance(string rawUrl);
	}

	public class UrlToTreeNodeSummaryMapper : IUrlToTreeNodeSummaryMapper
	{
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

		public UrlToTreeNodeSummaryMapper(ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
		}

		public TreeNodeSummary CreateInstance(string rawUrl)
		{
			var nodeSegments = ScrubUrlAndReturnEnumerableOfNodeSegments(rawUrl);

			TreeNodeSummary treeNodeSummary = null;

			var workingTreeNodeId = Constants.RootNodeId;
			foreach(var nodeSegment in nodeSegments)
			{
				treeNodeSummary = treeNodeSummaryContext.GetChildren(workingTreeNodeId).Where(a => a.UrlSegment == nodeSegment).FirstOrDefault();
				if (treeNodeSummary == null) return null;
				workingTreeNodeId = treeNodeSummary.Id;
			}

			return treeNodeSummary;
		}

		private static string[] ScrubUrlAndReturnEnumerableOfNodeSegments(string rawUrl)
		{
			if (rawUrl == null) rawUrl = string.Empty;
			if ((rawUrl.StartsWith("/")) && (rawUrl.Length > 1)) rawUrl = rawUrl.Substring(1, rawUrl.Length - 1);
			if (rawUrl.Contains("?"))
				rawUrl = rawUrl.Split('?')[0];
			return rawUrl.Split('/');
		}

	}
}