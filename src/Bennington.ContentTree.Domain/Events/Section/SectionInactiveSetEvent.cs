using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionInactiveSetEvent : DomainEvent
	{
		public bool Inactive { get; set; }
	}
}
