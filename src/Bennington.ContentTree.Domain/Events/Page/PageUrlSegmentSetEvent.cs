using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PageUrlSegmentSetEvent : DomainEvent
	{
		public string UrlSegment { get; set; }
	}
}
