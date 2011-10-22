using System;
using System.Collections.Generic;
using Bennington.ContentTree.Models;

namespace Bennington.ContentTree.TreeManager.Models
{
	public class TreeBranchViewModel
	{
        public IEnumerable<TreeBranchItemViewModel> TreeNodeSummaries { get; set; }
	}

    public class TreeBranchItemViewModel
    {
        public TreeNodeSummary TreeNodeSummary { get; set; }
        public bool IsActive { get; set; }
        public int Version { get; set; }
    }
}