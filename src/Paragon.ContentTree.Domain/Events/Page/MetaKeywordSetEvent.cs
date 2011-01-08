using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Page
{
	public class MetaKeywordSetEvent : DomainEvent
	{
		public string MetaKeyword { get; set; }
	}
}
