using System;
using System.Collections.Generic;
using System.Linq;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.Contexts
{
	public interface ITreeNodeSummaryContext
	{
		string Create(string parentNodeId, Type providerType);
		IEnumerable<TreeNodeSummary> GetChildren(string parentNodeId);
		string Create(string parentNodeId, string providerType);
	}

	public class TreeNodeSummaryContext : ITreeNodeSummaryContext
	{
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly ITreeNodeProviderContext treeNodeProviderContext;

		public TreeNodeSummaryContext(ITreeNodeRepository treeNodeRepository, ITreeNodeProviderContext treeNodeProviderContext)
		{
			this.treeNodeProviderContext = treeNodeProviderContext;
			this.treeNodeRepository = treeNodeRepository;
		}

		public string Create(string parentNodeId, Type providerType)
		{
			ThrowExceptionIfTheProviderTypeDoesNotImplementIAmATreeNodeExtensionProvider(providerType);

			return Create(parentNodeId, providerType.FullName);
		}

		public string Create(string parentNodeId, string providerType)
		{
			var newTreeNode = treeNodeRepository.Create(new TreeNode()
			{
				Id = Guid.NewGuid().ToString(),
				Type = providerType,
				ParentTreeNodeId = parentNodeId,
			});
			return newTreeNode == null ? string.Empty : newTreeNode.Id;
		}

		private static void ThrowExceptionIfTheProviderTypeDoesNotImplementIAmATreeNodeExtensionProvider(Type providerType)
		{
			if (!typeof(IAmATreeNodeExtensionProvider).IsAssignableFrom(providerType))
				throw new Exception(string.Format("Provider type must implement {0}", typeof(IAmATreeNodeExtensionProvider).FullName));
		}

		public IEnumerable<TreeNodeSummary> GetChildren(string parentNodeId)
		{
			var allNodes = treeNodeRepository.GetAll();
			var childNodes = from node in allNodes
							 where (node.ParentTreeNodeId == parentNodeId)
							 select GetTreeNodeSummaryForTreeNode(node);
			return childNodes;
		}

		private TreeNodeSummary GetTreeNodeSummaryForTreeNode(TreeNode treeNode)
		{
			var provider = treeNodeProviderContext.GetProviderByTypeName(treeNode.Type);
			if (provider == null) throw new Exception(string.Format("Content tree node provider for type: {0} not found.", treeNode.Type));

			var node = provider.GetAll().Where(a => a.TreeNodeId == treeNode.Id).FirstOrDefault();
			if (node == null) throw new Exception(string.Format("Node with id \"{0}\" was not found by provider type \"{1}\".", treeNode.Id, provider.GetType().AssemblyQualifiedName));

			var treeNodeSummary = new TreeNodeSummary()
			       	{
						Name = node.Name,
						Id = treeNode.Id,
						UrlSegment = node.UrlSegment,
						HasChildren = treeNodeRepository.GetAll().Where(a => a.ParentTreeNodeId == treeNode.Id).Any(),
						ControllerToUseForModification = provider.ControllerToUseForModification,
						ActionToUseForModification = provider.ActionToUseForModification,
						ControllerToUseForCreation = provider.ControllerToUseForCreation,
						ActionToUseForCreation = provider.ActionToUseForCreation,
						RouteValuesForModification = new { TreeNodeId = treeNode.Id },
						RouteValuesForCreation = new { ParentTreeNodeId = treeNode.Id },
						ParentTreeNodeId = treeNode.Id,
						Sequence = node.Sequence,
			       	};
			return treeNodeSummary;
		}
	}
}