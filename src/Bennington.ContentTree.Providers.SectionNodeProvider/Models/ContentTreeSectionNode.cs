using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.SectionNodeProvider.Models
{
	public class ContentTreeSectionNode : IAmATreeNodeExtension
	{
		public string SectionId { get; set; }
		public string TreeNodeId { get; set; }
		public string Name { get; set; }
		public string UrlSegment { get; set; }
		public string DefaultTreeNodeId { get; set; }
		public int? Sequence { get; set; }
	}
}
