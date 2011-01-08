using System;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Section
{
	public class DefaultPageSetEvent : DomainEvent
	{
		public Guid PageId { get; set; }
	}
}
