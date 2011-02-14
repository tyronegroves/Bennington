using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Section
{
	public class SectionUrlSegmentSetEvent : DomainEvent
	{
		public string UrlSegment { get; set; }
	}
}
