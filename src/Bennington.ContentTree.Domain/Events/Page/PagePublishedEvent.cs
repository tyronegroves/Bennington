using System;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class PagePublishedEvent : DomainEvent
	{
		public Guid Id { get; set; }
	}
}
