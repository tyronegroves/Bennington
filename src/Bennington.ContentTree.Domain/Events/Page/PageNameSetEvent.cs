using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class PageNameSetEvent : DomainEvent
	{
		public string Name { get; set; }
	}
}
