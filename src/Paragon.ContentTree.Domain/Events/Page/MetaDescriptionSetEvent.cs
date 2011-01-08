using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class MetaDescriptionSetEvent : DomainEvent
	{
		public string MetaDescription { get; set; }
	}
}
