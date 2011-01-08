using System;
using Paragon.ContentTree.Domain.Events;
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
			Apply(new UrlSegmentSetEvent() { UrlSegment = urlSegment });
		}

		public void SetIsActive(bool isActive)
		{
			Apply(new IsActiveSetEvent() { IsActive = isActive });
		}

		public void SetIsVisible(bool isVisible)
		{
			Apply(new IsVisibleSetEvent() { IsVisible = isVisible });
		}

		public void SetParentTreeNodeId(Guid parentTreeNodeId)
		{
			Apply(new ParentTreeNodeIdSetEvent() { ParentTreeNodeId = parentTreeNodeId });
		}

		public void SetSequence(int? sequence)
		{
			Apply(new SequenceSetEvent() { Sequence = sequence });
		}
	}
}
