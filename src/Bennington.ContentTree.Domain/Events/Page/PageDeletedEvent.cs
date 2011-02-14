using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PageDeletedEvent : DomainEvent
	{
		public Guid TreeNodeId { get; set; }
	}
}
