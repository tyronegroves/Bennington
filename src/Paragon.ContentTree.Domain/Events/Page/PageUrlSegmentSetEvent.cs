using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class PageUrlSegmentSetEvent : DomainEvent
	{
		public string UrlSegment { get; set; }
	}
}
