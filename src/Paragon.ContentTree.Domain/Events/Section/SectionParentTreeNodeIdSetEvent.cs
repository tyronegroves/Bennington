using System;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Section
{
	public class SectionParentTreeNodeIdSetEvent : DomainEvent
	{
		public Guid ParentTreeNodeId { get; set; }
	}
}
