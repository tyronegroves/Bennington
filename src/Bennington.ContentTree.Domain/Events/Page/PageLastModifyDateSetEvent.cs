using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
    public class PageLastModifyDateSetEvent : DomainEvent
    {
        public DateTime DateTime { get; set; }
    }
}