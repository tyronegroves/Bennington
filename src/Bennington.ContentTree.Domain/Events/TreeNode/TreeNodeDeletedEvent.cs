using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.TreeNode
{
	public class TreeNodeDeletedEvent : DomainEvent
	{
		public Guid TreeNodeId { get; set; }
	}
}
