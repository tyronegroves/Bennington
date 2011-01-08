using System;
using Paragon.ContentTree.Domain.Events;
using Paragon.ContentTree.Domain.Events.Section;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.AggregateRoots
{
	public class Section : SimpleCqrs.Domain.AggregateRoot
	{
		public Section()
		{
		}

		public Section(Guid sectionId)
		{
			Apply(new SectionCreatedEvent(){ AggregateRootId = sectionId });
		}

		public void OnSectionCreatedEvent(SectionCreatedEvent sectionCreatedEvent)
		{
			Id = sectionCreatedEvent.AggregateRootId;
		}

		public void SetName(string name)
		{
			Apply(new SectionNameSetEvent() { Name = name });
		}

		public void SetDefaultPage(Guid pageId)
		{
			Apply(new DefaultPageSetEvent(){ PageId = pageId });
		}

		public void SetUrlSegment(string urlSegment)
		{
			Apply(new SectionUrlSegmentSetEvent() { UrlSegment = urlSegment });
		}

		public void SetIsActive(bool isActive)
		{
			Apply(new SectionIsActiveSetEvent() { IsActive = isActive });
		}

		public void SetIsVisible(bool isVisible)
		{
			Apply(new SectionIsVisibleSetEvent() { IsVisible = isVisible });
		}

		public void SetParentTreeNodeId(Guid parentTreeNodeId)
		{
			Apply(new SectionParentTreeNodeIdSetEvent() { ParentTreeNodeId = parentTreeNodeId });
		}

		public void SetSequence(int? sequence)
		{
			Apply(new SectionSequenceSetEvent() { SectionSequence = sequence });
		}
	}
}