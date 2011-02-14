using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionCreatedEvent : DomainEvent
	{
		public Guid SectionId { get; set; }
	}
}
