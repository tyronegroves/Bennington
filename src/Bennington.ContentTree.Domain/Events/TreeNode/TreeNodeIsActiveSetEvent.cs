using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeIsActiveSetEvent : DomainEvent
	{
		public bool IsActive { get; set; }
	}
}