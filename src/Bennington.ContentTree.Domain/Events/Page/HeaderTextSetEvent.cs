using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class HeaderTextSetEvent : DomainEvent
	{
		public string HeaderText { get; set; }
	}
}
