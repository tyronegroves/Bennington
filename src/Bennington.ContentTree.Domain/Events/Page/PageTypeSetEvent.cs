using System;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class PageTypeSetEvent : DomainEvent
	{
		public Type Type { get; set; }
	}
}
