using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionSequenceSetEvent : DomainEvent
	{
		public int? SectionSequence { get; set; }
	}
}
