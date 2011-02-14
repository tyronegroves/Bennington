using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PageActionSetEvent : DomainEvent
	{
		public string Action { get; set; }
	}
}
