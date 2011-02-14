using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PagePublishedEvent : DomainEvent
	{
		public Guid Id { get; set; }
	}
}
