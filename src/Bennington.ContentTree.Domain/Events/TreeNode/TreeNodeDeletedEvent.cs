using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeDeletedEvent : DomainEvent
	{
		public Guid TreeNodeId { get; set; }
	}
}
