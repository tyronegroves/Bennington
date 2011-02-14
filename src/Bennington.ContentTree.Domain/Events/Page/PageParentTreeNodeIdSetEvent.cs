using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PageParentTreeNodeIdSetEvent : DomainEvent
	{
		public Guid ParentTreeNodeId { get; set; }
	}
}
