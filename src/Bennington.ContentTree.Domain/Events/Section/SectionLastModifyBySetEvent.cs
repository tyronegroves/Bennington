using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
    public class SectionLastModifyBySetEvent : DomainEvent
    {
        public string LastModifyBy { get; set; }
    }
}