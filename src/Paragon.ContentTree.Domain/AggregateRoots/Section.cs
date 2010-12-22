using System;
using Paragon.ContentTree.Domain.Events;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.AggregateRoots
{
	public class Section : SimpleCqrs.Domain.AggregateRoot
	{
		public Section()
		{
		}

		public Section(Guid pageId)
		{
			Apply(new SectionCreatedEvent(){ AggregateRootId = pageId });
		}

		public void OnSectionCreatedEvent(SectionCreatedEvent sectionCreatedEvent)
		{
			Id = sectionCreatedEvent.AggregateRootId;
		}

		public void SetName(string name)
		{
			Apply(new NameSetEvent(){});
		}

		public void SetUrlSegment(string urlSegment)
		{
			Apply(new UrlSegmentSetEvent(){ UrlSegment = urlSegment });
		}

		public void SetDefaultPage(string pageId)
		{
			Apply(new DefaultPageSetEvent(){ PageId = pageId });
		}

		public void SetParentId(string parentId)
		{
			Apply(new ParentIdSetEvent(){ ParentId = parentId });
		}

		public void SetSequence(int? sequence)
		{
			Apply(new SequenceSetEvent(){ Sequence = sequence });
		}
	}
}