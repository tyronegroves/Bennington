using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeCreatedEvent : DomainEvent
	{
		public Guid ParentTreeNodeId { get; set; }
		public Type Type { get; set; }
	}
}
