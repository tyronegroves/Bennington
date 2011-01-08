using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeUrlSegmentSetEvent : DomainEvent
	{
		public string UrlSegment { get; set; }
	}
}
