using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
    public class PageLastModifyBySetEvent : DomainEvent
    {
        public string LastModifyBy { get; set; }
    }
}