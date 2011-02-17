using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionHiddenSetEvent : DomainEvent
	{
		public bool Hidden { get; set; }
	}
}
