using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Paragon.ContentTree.Contexts;

namespace Paragon.ContentTree.Helpers
{
	public interface ITreeNodeIdToUrl
	{
		string GetUrlByTreeNodeId(string treeNodeId);
	}

	public class TreeNodeIdToUrl : ITreeNodeIdToUrl
	{
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

		public TreeNodeIdToUrl(ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
		}

		public string GetUrlByTreeNodeId(string treeNodeId)
		{
			var treeNodeSummary = treeNodeSummaryContext.GetTreeNodeSummaryByTreeNodeId(treeNodeId);
			if (treeNodeSummary == null) return null;

			var segments = new List<string>();
			do
			{
				segments.Add(treeNodeSummary.UrlSegment);
				treeNodeSummary = treeNodeSummaryContext.GetTreeNodeSummaryByTreeNodeId(treeNodeSummary.ParentTreeNodeId);
			} while (treeNodeSummary != null);


			segments.Reverse();
			var stringBuilder = new StringBuilder("/");
			foreach(var segment in segments)
			{
				stringBuilder.Append(segment + "/");
			}

			var url = stringBuilder.ToString();
			return url.Substring(0, url.Length - 1);
		}
	}
}