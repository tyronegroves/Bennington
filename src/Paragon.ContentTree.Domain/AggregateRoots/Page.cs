using System;
using Paragon.ContentTree.Domain.Events;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.AggregateRoots
{
	public class Page : SimpleCqrs.Domain.AggregateRoot
	{
		public Page(Guid pageId)
		{
			Apply(new PageCreatedEvent(){ AggregateRootId = pageId });
		}

		protected void OnPagetCreated(PageCreatedEvent pageCreatedEvent)
		{
			Id = pageCreatedEvent.AggregateRootId;
		}

		public void SetHeaderText(string headerText)
		{
			Apply(new HeaderTextSetEvent() { HeaderText = headerText });
		}

		public void SetBody(string body)
		{
			Apply(new BodySetEvent() { Body = body });
		}

		public void SetUrlSegment(string urlSegment)
		{
			Apply(new UrlSegmentSetEvent() { UrlSegment = urlSegment });
		}

		public void SetParentId(string parentId)
		{
			Apply(new ParentIdSetEvent() { ParentId = parentId });
		}

		public void SetSequence(int? sequence)
		{
			Apply(new SequenceSetEvent() { Sequence = sequence });
		}

		public void Publish()
		{
			throw new NotImplementedException();
		}

		public void Revert(int versionNumber)
		{
			throw new NotImplementedException();
		}
	}
}
