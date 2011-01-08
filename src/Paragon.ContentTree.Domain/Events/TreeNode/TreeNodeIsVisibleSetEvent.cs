using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeIsVisibleSetEvent : DomainEvent
	{
		public bool IsVisible { get; set; }
	}
}
