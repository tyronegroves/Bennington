using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PageHeaderImageSetEvent : DomainEvent
	{
		public string HeaderImage { get; set; }
	}
}
