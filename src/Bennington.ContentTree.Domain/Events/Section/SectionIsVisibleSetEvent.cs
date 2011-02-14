using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionIsVisibleSetEvent : DomainEvent
	{
		public bool IsVisible { get; set; }
	}
}
