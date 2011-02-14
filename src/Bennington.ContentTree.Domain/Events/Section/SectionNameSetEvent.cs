using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionNameSetEvent : DomainEvent
	{
		public string Name { get; set; }
	}
}
