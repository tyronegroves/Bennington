using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Section
{
	public class SectionNameSetEvent : DomainEvent
	{
		public string Name { get; set; }
	}
}
