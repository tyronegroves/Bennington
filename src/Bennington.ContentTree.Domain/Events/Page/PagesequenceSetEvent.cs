using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class PageSequenceSetEvent : DomainEvent
	{
		public int? PageSequence { get; set; }
	}
}
