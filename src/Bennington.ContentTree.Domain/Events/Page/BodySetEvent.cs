using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class BodySetEvent : DomainEvent
	{
		public string Body { get; set; }
	}
}
