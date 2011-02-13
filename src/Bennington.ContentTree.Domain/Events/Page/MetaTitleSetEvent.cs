using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class MetaTitleSetEvent : DomainEvent
	{
		public string MetaTitle { get; set; }
	}
}
