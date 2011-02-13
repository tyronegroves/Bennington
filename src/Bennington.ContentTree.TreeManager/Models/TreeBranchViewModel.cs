using System.Collections.Generic;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.TreeManager.Models
{
	public class TreeBranchViewModel
	{
		public IEnumerable<TreeNodeSummary> TreeNodeSummaries { get; set; }
	}
}