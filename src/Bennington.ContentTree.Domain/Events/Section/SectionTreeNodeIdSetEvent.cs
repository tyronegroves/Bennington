using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Section
{
	public class SectionTreeNodeIdSetEvent : DomainEvent
	{
		public Guid TreeNodeId { get; set; }
	}
}
