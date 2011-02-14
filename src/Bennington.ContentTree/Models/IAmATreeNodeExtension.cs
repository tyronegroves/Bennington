namespace Bennington.ContentTree.Models
{
	public interface IAmATreeNodeExtension
	{
		string TreeNodeId { get; set; }
		int? Sequence { get; set; }
		string UrlSegment { get; set; }
		string Name { get; set; }
	}
}
