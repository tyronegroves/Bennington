using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Section
{
	public class SectionSequenceSetEvent : DomainEvent
	{
		public int? SectionSequence { get; set; }
	}
}
