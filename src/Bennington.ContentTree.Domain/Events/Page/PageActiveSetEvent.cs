using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PageInactiveSetEvent : DomainEvent
	{
		public bool Inactive { get; set; }
	}
}
