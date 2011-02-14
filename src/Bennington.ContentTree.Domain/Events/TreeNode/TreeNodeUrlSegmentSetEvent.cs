using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeUrlSegmentSetEvent : DomainEvent
	{
		public string UrlSegment { get; set; }
	}
}
