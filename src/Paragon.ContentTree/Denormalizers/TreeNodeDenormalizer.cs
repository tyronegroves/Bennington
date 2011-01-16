﻿using System;
using System.Linq;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Domain.Events.Page;
using Paragon.ContentTree.Domain.Events.TreeNode;
using Paragon.ContentTree.Repositories;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.DeNormalizers
{
	public class TreeNodeDenormalizer : IHandleDomainEvents<PageDeletedEvent>,
										IHandleDomainEvents<PageParentTreeNodeIdSetEvent>,
										IHandleDomainEvents<PageTypeSetEvent>,
										IHandleDomainEvents<TreeNodeCreatedEvent>
	{
		private readonly ITreeNodeRepository treeNodeRepository;

		public TreeNodeDenormalizer(ITreeNodeRepository treeNodeRepository)
		{
			this.treeNodeRepository = treeNodeRepository;
		}

		public void Handle(PageParentTreeNodeIdSetEvent domainEvent)
		{
			var treeNode = GetTreeNodeFromDomainEvent(domainEvent);
			treeNode.ParentTreeNodeId = domainEvent.ParentTreeNodeId.ToString();
			treeNodeRepository.Update(treeNode);
		}

		private TreeNode GetTreeNodeFromDomainEvent(DomainEvent domainEvent)
		{
			return treeNodeRepository.GetAll().Where(a => a.Id == domainEvent.AggregateRootId.ToString()).FirstOrDefault();
		}

		public void Handle(PageDeletedEvent domainEvent)
		{
			treeNodeRepository.Delete(domainEvent.AggregateRootId.ToString());
		}

		public void Handle(PageTypeSetEvent domainEvent)
		{
			var treeNode = GetTreeNodeFromDomainEvent(domainEvent);
			treeNode.Type = domainEvent.Type.AssemblyQualifiedName;
			treeNodeRepository.Update(treeNode);
		}

		public void Handle(TreeNodeCreatedEvent domainEvent)
		{
			treeNodeRepository.Create(new TreeNode()
										{
											Id = domainEvent.AggregateRootId.ToString(),
										});
		}

		public void Handle(TreeNodeTypeSetEvent domainEvent)
		{
			var treeNode = GetTreeNodeFromDomainEvent(domainEvent);
			treeNode.Type = domainEvent.Type.AssemblyQualifiedName;
			treeNodeRepository.Update(treeNode);
		}

		public void Handle(TreeNodeParentTreeNodeIdSetEvent domainEvent)
		{
			var treeNode = GetTreeNodeFromDomainEvent(domainEvent);
			treeNode.ParentTreeNodeId = domainEvent.ParentTreeNodeId.ToString();
			treeNodeRepository.Update(treeNode);
		}
	}
}