using System.Collections.Generic;
using System.Linq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Models;
using Bennington.ContentTree.TreeManager.Models;

namespace Bennington.ContentTree.TreeManager.ViewModelBuilders
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
            var listToReturn = new List<TreeBranchItemViewModel>();
			var treeNodeSummaries = treeNodeSummaryContext.GetChildren(parentNodeId).OrderBy(a => a.Sequence ?? 999999);
			foreach (var treeNodeSummary in treeNodeSummaries)
			{
				if (string.IsNullOrEmpty(treeNodeSummary.Name))
					treeNodeSummary.Name = "Unknown";
				listToReturn.Add(new TreeBranchItemViewModel()
				                     {
                                         TreeNodeSummary = treeNodeSummary,
                                         IsActive = true,
                                         Version = 1
				                     });
			}
			return new TreeBranchViewModel()
			       	{
			       		TreeNodeSummaries = listToReturn,
			       	};
		}
	}
}