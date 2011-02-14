using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeIsActiveSetEvent : DomainEvent
	{
		public bool IsActive { get; set; }
	}
}