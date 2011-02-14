using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PageSequenceSetEvent : DomainEvent
	{
		public int? PageSequence { get; set; }
	}
}
