using Paragon.ContentTree.Models;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models
{
	public class ToolLink : IAmATreeNodeExtension
	{
		public string Name { get; set; }
		public string UrlSegment { get; set; }
		public string TreeNodeId { get; set; }
		public int? Sequence { get; set; }
	}
}