using System;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class PageParentTreeNodeIdSetEvent : DomainEvent
	{
		public Guid ParentTreeNodeId { get; set; }
	}
}
