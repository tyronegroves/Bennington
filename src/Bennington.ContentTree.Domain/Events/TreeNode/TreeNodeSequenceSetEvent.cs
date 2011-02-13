using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeSequenceSetEvent : DomainEvent
	{
		public int? TreeNodeSequence { get; set; }
	}
}
