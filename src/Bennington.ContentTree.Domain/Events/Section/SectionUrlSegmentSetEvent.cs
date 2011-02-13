using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Section
{
	public class SectionUrlSegmentSetEvent : DomainEvent
	{
		public string UrlSegment { get; set; }
	}
}
