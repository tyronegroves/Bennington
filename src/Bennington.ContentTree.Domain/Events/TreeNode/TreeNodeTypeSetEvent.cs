using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeTypeSetEvent : DomainEvent
	{
		public Type Type { get; set; }
	}
}
