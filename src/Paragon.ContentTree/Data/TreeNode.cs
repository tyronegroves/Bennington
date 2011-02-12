using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paragon.ContentTree.Data
{
	public class TreeNode
	{
		public string Id { get; set; }
		public string ParentTreeNodeId {get; set;}
		public string Type {get; set;}
	}
}