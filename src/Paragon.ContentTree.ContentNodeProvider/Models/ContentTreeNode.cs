using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.ContentNodeProvider.Models
{
	public class ContentTreeNode : IAmATreeNodeExtension
	{
		public int Key { get; set; }
		public string TreeNodeId { get; set; }
		public string PageId { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string Name { get; set; }
		public string Body { get; set; }
		public string Action { get; set; }
		public string HeaderText { get; set; }
	}
}
