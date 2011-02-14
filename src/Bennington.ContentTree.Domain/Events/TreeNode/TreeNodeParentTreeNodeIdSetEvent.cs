using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeParentTreeNodeIdSetEvent : DomainEvent
	{
		public Guid ParentTreeNodeId { get; set; }
	}
}
