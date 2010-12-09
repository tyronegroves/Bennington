using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.ViewModelBuilders
{
	public interface ITreeBranchViewModelBuilder
	{
		TreeBranchViewModel BuildViewModel(string parentNodeId);
	}

	public class TreeBranchViewModelBuilder : ITreeBranchViewModelBuilder
	{
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

		public TreeBranchViewModelBuilder(ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
		}

		public TreeBranchViewModel BuildViewModel(string parentNodeId)
		{
			var treeNodeSummaries = treeNodeSummaryContext.GetChildren(parentNodeId).OrderBy(a => a.Sequence ?? 999999);
			foreach (var treeNodeSummary in treeNodeSummaries)
			{
				if (string.IsNullOrEmpty(treeNodeSummary.Name))
					treeNodeSummary.Name = "Unknown";
			}
			return new TreeBranchViewModel()
			       	{
			       		TreeNodeSummaries = treeNodeSummaries,
			       	};
		}
	}
}