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
			Id = sectionId;
			Apply(new SectionCreatedEvent(){ AggregateRootId = sectionId, SectionId =  sectionId });
		}

		public void OnSectionCreatedEvent(SectionCreatedEvent sectionCreatedEvent)
		{
			Id = sectionCreatedEvent.AggregateRootId;
		}

		public void SetName(string name)
		{
			Apply(new SectionNameSetEvent() { AggregateRootId = Id, Name = name });
		}

		public void SetDefaultTreeNodeId(Guid pageId)
		{
			Apply(new SectionDefaultTreeNodeIdSetEvent(){ AggregateRootId  = Id, DefaultTreeNodeId = pageId });
		}

		public void SetUrlSegment(string urlSegment)
		{
			Apply(new SectionUrlSegmentSetEvent() { AggregateRootId = Id, UrlSegment = urlSegment });
		}

		public void SetIsActive(bool isActive)
		{
			Apply(new SectionIsActiveSetEvent() { AggregateRootId = Id, IsActive = isActive });
		}

		public void SetIsVisible(bool isVisible)
		{
			Apply(new SectionIsVisibleSetEvent() { AggregateRootId = Id, IsVisible = isVisible });
		}

		public void SetParentTreeNodeId(Guid parentTreeNodeId)
		{
			Apply(new SectionParentTreeNodeIdSetEvent() { AggregateRootId = Id, ParentTreeNodeId = parentTreeNodeId });
		}

		public void SetSequence(int? sequence)
		{
			Apply(new SectionSequenceSetEvent() { AggregateRootId = Id, SectionSequence = sequence });
		}

		public void Delete()
		{
			Apply(new SectionDeletedEvent() { AggregateRootId = Id });
		}
	}
}