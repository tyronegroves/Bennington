using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionTreeNodeIdSetEvent : DomainEvent
	{
		public Guid TreeNodeId { get; set; }
	}
}
