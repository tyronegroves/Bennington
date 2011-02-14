using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.Commands
{
	public class CreateSectionCommand : CommandWithAggregateRootId
	{
		public string SectionId { get; set; }
		public string TreeNodeId { get; set; }
		public string ParentTreeNodeId { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string DefaultTreeNodeId { get; set; }
		public string Name { get; set; }
	}
}
