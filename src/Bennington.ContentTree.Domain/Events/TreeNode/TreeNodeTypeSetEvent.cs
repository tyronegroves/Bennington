using System;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeTypeSetEvent : DomainEvent
	{
		public Type Type { get; set; }
	}
}
