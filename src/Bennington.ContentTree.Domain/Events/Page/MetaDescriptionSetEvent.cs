using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class MetaDescriptionSetEvent : DomainEvent
	{
		public string MetaDescription { get; set; }
	}
}
