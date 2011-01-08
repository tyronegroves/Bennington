using System;
using Paragon.ContentTree.Domain.Events;
using Paragon.ContentTree.Domain.Events.Page;
using Paragon.ContentTree.Domain.Events.TreeNode;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.AggregateRoots
{
	public class Page : SimpleCqrs.Domain.AggregateRoot
	{
		public Page(Guid pageId)
		{
			Apply(new PageCreatedEvent(){ AggregateRootId = pageId });
		}

		public Page()
		{
		}

		protected void OnPagetCreated(PageCreatedEvent pageCreatedEvent)
		{
			Id = pageCreatedEvent.AggregateRootId;
		}

		public void SetName(string name)
		{
			Apply(new PageNameSetEvent() { Name = name });
		}

		public void SetActionId(string stepId)
		{
			Apply(new PageActionSetEvent());
		}

		public void SetParentTreeNodeId(Guid parentTreeNodeId)
		{
			Apply(new PageParentTreeNodeIdSetEvent(){ ParentTreeNodeId = parentTreeNodeId });
		}

		public void SetMetaTitle(string metaTitle)
		{
			Apply(new MetaTitleSetEvent(){ MetaTitle = metaTitle });
		}

		public void SetMetaDescription(string metaDescription)
		{
			Apply(new MetaDescriptionSetEvent() { MetaDescription = metaDescription });
		}

		public void SetMetaKeyword(string metaKeyword)
		{
			Apply(new MetaKeywordSetEvent() { MetaKeyword = metaKeyword });
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
			Apply(new PageUrlSegmentSetEvent() { UrlSegment = urlSegment });
		}

		public void SetTreeNodeId(Guid treeNodeId)
		{
			Apply(new TreeNodeIdSetEvent() { TreeNodeId  = treeNodeId });
		}

		public void Publish()
		{
			Apply(new PagePublishedEvent(){ Id = Id });
		}
	}
}
