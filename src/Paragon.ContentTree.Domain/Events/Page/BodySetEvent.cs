using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class BodySetEvent : DomainEvent
	{
		public string Body { get; set; }
	}
}
