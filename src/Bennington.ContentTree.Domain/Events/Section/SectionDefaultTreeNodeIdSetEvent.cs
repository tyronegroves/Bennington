using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionDefaultTreeNodeIdSetEvent : DomainEvent
	{
		public Guid DefaultTreeNodeId { get; set; }
	}
}
