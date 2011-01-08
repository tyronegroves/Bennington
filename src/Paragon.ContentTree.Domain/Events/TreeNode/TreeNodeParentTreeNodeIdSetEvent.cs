using System;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeParentTreeNodeIdSetEvent : DomainEvent
	{
		public Guid ParentTreeNodeId { get; set; }
	}
}
