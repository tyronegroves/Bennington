using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class MetaTitleSetEvent : DomainEvent
	{
		public string MetaTitle { get; set; }
	}
}
