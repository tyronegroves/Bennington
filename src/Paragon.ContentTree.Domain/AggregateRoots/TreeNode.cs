using System;
using Paragon.ContentTree.Domain.Events;
using Paragon.ContentTree.Domain.Events.TreeNode;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.Domain.AggregateRoots
{
	public class TreeNode : SimpleCqrs.Domain.AggregateRoot
	{
		public TreeNode(Guid treeNodeId)
		{
			Apply(new TreeNodeCreatedEvent(){ AggregateRootId = treeNodeId });
		}

		public TreeNode()
		{
		}

		protected void OnTreeNodeCreated(TreeNodeCreatedEvent treeNodeCreatedEvent)
		{
			Id = treeNodeCreatedEvent.AggregateRootId;
		}

		public void SetUrlSegment(string urlSegment)
		{
			Apply(new TreeNodeUrlSegmentSetEvent() { UrlSegment = urlSegment });
		}

		public void SetIsActive(bool isActive)
		{
			Apply(new TreeNodeIsActiveSetEvent() { IsActive = isActive });
		}

		public void SetIsVisible(bool isVisible)
		{
			Apply(new TreeNodeIsVisibleSetEvent() { IsVisible = isVisible });
		}

		public void SetParentTreeNodeId(Guid parentTreeNodeId)
		{
			Apply(new TreeNodeParentTreeNodeIdSetEvent() { ParentTreeNodeId = parentTreeNodeId });
		}

		public void SetSequence(int? sequence)
		{
			Apply(new TreeNodeSequenceSetEvent() { TreeNodeSequence = sequence });
		}

		public void SetType(Type type)
		{
			Apply(new TreeNodeTypeSetEvent(){Type = type});
		}

		public void Delete(Guid aggregateRootId)
		{
			Apply(new TreeNodeDeletedEvent()
			      	{
			      		AggregateRootId = aggregateRootId,
						TreeNodeId = aggregateRootId,
			      	});
		}
	}
}