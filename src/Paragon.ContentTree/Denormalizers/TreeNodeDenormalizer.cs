using System;
using System.Linq;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Domain.Events.Page;
using Paragon.ContentTree.Repositories;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.DeNormalizers
{
	public class TreeNodeDenormalizer : IHandleDomainEvents<PageCreatedEvent>,
										IHandleDomainEvents<PageParentTreeNodeIdSetEvent>
	{
		private readonly ITreeNodeRepository treeNodeRepository;

		public TreeNodeDenormalizer(ITreeNodeRepository treeNodeRepository)
		{
			this.treeNodeRepository = treeNodeRepository;
		}

		public void Handle(PageCreatedEvent domainEvent)
		{
			treeNodeRepository.Create(new TreeNode()
			                          	{
			                          		Id = domainEvent.AggregateRootId.ToString(),
											Type = domainEvent.ProviderType.AssemblyQualifiedName,
			                          	});
		}

		public void Handle(PageParentTreeNodeIdSetEvent domainEvent)
		{
			var treeNode = treeNodeRepository.GetAll().Where(a => a.Id == domainEvent.AggregateRootId.ToString()).FirstOrDefault();
			treeNode.ParentTreeNodeId = domainEvent.ParentTreeNodeId.ToString();
			treeNodeRepository.Update(treeNode);
		}
	}
}