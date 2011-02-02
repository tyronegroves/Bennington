using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.Helpers
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