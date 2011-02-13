using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class HeaderTextSetEvent : DomainEvent
	{
		public string HeaderText { get; set; }
	}
}
