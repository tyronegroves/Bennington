using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PageTreeNodeIdSetEvent : DomainEvent
	{
		public Guid TreeNodeId { get; set; }
	}
}
