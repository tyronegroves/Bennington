using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class PageActionSetEvent : DomainEvent
	{
		public string Action { get; set; }
	}
}
