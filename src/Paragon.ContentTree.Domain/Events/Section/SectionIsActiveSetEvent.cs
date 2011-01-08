using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.Events.Section
{
	public class SectionIsActiveSetEvent : DomainEvent
	{
		public bool IsActive { get; set; }
	}
}
