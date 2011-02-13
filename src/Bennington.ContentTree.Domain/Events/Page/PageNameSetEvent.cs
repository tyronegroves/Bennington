using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class PageNameSetEvent : DomainEvent
	{
		public string Name { get; set; }
	}
}
