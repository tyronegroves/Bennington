using System;
using Bennington.ContentTree.Domain.Events.Section;

namespace Bennington.ContentTree.Domain.AggregateRoots
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

		public Guid SectionId
		{
			get { return Id; }
			set { Id = value; }
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

		public void SetInactive(bool inactive)
		{
			Apply(new SectionInactiveSetEvent() { AggregateRootId = Id, Inactive = inactive });
		}

		public void SetHidden(bool hidden)
		{
			Apply(new SectionHiddenSetEvent() { AggregateRootId = Id, Hidden = hidden });
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

		public void SetTreeNodeId(Guid guid)
		{
			Apply(new SectionTreeNodeIdSetEvent()
			      	{
			      		AggregateRootId = Id,
						TreeNodeId = guid,
			      	});
		}
	}
}