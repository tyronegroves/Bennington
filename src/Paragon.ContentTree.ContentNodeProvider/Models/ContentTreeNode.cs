using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.ContentNodeProvider.Models
{
	public class ContentTreeNode : IAmATreeNodeExtension
	{
		public int Key { get; set; }
		public string TreeNodeId { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }
		public string ContentItemId { get; set; }
		public string HeaderText { get; set; }
	}
}
