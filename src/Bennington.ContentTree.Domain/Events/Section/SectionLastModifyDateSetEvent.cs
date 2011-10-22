using System;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
    public class SectionLastModifyDateSetEvent : DomainEvent
    {
        public DateTime LastModifyDate { get; set; }
    }
}