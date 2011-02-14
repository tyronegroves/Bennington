using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Domain.Events.Page
{
	public class MetaKeywordSetEvent : DomainEvent
	{
		public string MetaKeyword { get; set; }
	}
}
