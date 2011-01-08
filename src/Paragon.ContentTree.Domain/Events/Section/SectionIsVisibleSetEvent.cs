using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Section
{
	public class SectionIsVisibleSetEvent : DomainEvent
	{
		public bool IsVisible { get; set; }
	}
}
