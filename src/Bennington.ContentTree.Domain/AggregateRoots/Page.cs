using System;
using Bennington.ContentTree.Domain.Events.Page;

namespace Bennington.ContentTree.Domain.AggregateRoots
{
	public class Page : SimpleCqrs.Domain.AggregateRoot
	{
		public Page(Guid pageId)
		{
			Id = pageId;
			Apply(new PageCreatedEvent(){ AggregateRootId = pageId });
		}

		public Page()
		{
		}

		public Guid PageId
		{
			get
			{
				return this.Id;
			}
			set
			{
				Id = value;
			}
		}

		protected void OnPageCreated(PageCreatedEvent pageCreatedEvent)
		{
			Id = pageCreatedEvent.AggregateRootId;
		}

		public void SetName(string name)
		{
			Apply(new PageNameSetEvent() { AggregateRootId = Id, Name = name });
		}

		public void SetHidden(bool hidden)
		{
			Apply(new PageHiddenSetEvent() { Hidden = hidden });
		}

		public void SetInactive(bool inactive)
		{
			Apply(new PageInactiveSetEvent(){ Inactive = inactive });
		}

		public void SetActionId(string stepId)
		{
			Apply(new PageActionSetEvent() { Action = stepId });
		}

		public void SetParentTreeNodeId(Guid parentTreeNodeId)
		{
			Apply(new PageParentTreeNodeIdSetEvent() { AggregateRootId = Id, ParentTreeNodeId = parentTreeNodeId });
		}

		public void SetMetaTitle(string metaTitle)
		{
			Apply(new MetaTitleSetEvent(){ MetaTitle = metaTitle });
		}

		public void SetMetaDescription(string metaDescription)
		{
			Apply(new MetaDescriptionSetEvent() { AggregateRootId = Id, MetaDescription = metaDescription });
		}

		public void SetMetaKeyword(string metaKeyword)
		{
			Apply(new MetaKeywordSetEvent() { AggregateRootId = Id, MetaKeyword = metaKeyword });
		}

		public void SetHeaderText(string headerText)
		{
			Apply(new HeaderTextSetEvent() { AggregateRootId = Id, HeaderText = headerText });
		}

		public void SetHeaderImage(string headerImage)
		{
			Apply(new PageHeaderImageSetEvent()
			      	{
						AggregateRootId = Id,
			      		HeaderImage = headerImage,
			      	});
		}

		public void SetBody(string body)
		{
			Apply(new BodySetEvent() { AggregateRootId = Id, Body = body });
		}

		public void SetUrlSegment(string urlSegment)
		{
			Apply(new PageUrlSegmentSetEvent() { AggregateRootId = Id, UrlSegment = urlSegment });
		}

		public void SetTreeNodeId(Guid treeNodeId)
		{
			Apply(new PageTreeNodeIdSetEvent() { AggregateRootId = Id, TreeNodeId = treeNodeId });
		}

		public void Publish()
		{
			Apply(new PagePublishedEvent() { AggregateRootId = Id, Id = Id });
		}

		public void Delete(Guid treeNodeId)
		{
			Apply(new PageDeletedEvent()
			      	{
			      		AggregateRootId = Id,
						TreeNodeId = treeNodeId,
			      	});
		}

		public void SetType(Type type)
		{
			Apply(new PageTypeSetEvent()
			      	{
						AggregateRootId = Id,
			      		Type = type
			      	});
		}

		public void SetSequence(int? sequence)
		{
			Apply(new PageSequenceSetEvent()
			{
				AggregateRootId = Id,
				PageSequence = sequence
			});
		}

        public void SetLastModifyBy(string lastModifyBy)
        {
            Apply(new PageLastModifyBySetEvent()
                      {
                          LastModifyBy = lastModifyBy,
                      });
        }
	}
}
