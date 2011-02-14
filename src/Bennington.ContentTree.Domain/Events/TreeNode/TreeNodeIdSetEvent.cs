using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeIdSetEvent : DomainEvent
	{
		public Guid TreeNodeId { get; set; }
	}
}
