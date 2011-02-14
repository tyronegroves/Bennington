using System.Collections.Generic;
using Bennington.ContentTree.Models;

namespace Bennington.ContentTree.TreeManager.Models
{
	public class TreeBranchViewModel
	{
		public IEnumerable<TreeNodeSummary> TreeNodeSummaries { get; set; }
	}
}