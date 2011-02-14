using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeIsVisibleSetEvent : DomainEvent
	{
		public bool IsVisible { get; set; }
	}
}
