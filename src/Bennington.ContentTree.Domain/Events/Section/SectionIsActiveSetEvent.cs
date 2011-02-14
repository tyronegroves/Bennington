using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionIsActiveSetEvent : DomainEvent
	{
		public bool IsActive { get; set; }
	}
}
