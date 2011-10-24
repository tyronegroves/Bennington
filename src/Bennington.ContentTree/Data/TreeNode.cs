using System;

namespace Bennington.ContentTree.Data
{
	public class TreeNode
	{
        public Guid SisoId { get; set; }

		public string Id { get; set; }

		public string ParentTreeNodeId {get; set;}

		public string Type {get; set;}
	}
}