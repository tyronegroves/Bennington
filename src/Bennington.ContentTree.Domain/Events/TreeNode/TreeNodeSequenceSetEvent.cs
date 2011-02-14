using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeSequenceSetEvent : DomainEvent
	{
		public int? TreeNodeSequence { get; set; }
	}
}
