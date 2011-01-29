using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Domain.Commands;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTree.TreeNodeExtensionProvider;
using Paragon.Core.Helpers;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Contexts
{
	public interface ITreeNodeSummaryContext
	{
		TreeNodeSummary GetTreeNodeSummaryByTreeNodeId(string nodeId);
		IEnumerable<TreeNodeSummary> GetChildren(string parentNodeId);
		string Create(string parentNodeId, string providerTypeAssemblyQualifiedName);
		void Delete(string nodeId);
	}

	public class TreeNodeSummaryContext : ITreeNodeSummaryContext
	{
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly ITreeNodeProviderContext treeNodeProviderContext;
		private readonly ICommandBus commandBus;
		private readonly IGuidGetter guidGetter;

		public TreeNodeSummaryContext(ITreeNodeRepository treeNodeRepository, 
										ITreeNodeProviderContext treeNodeProviderContext,
										ICommandBus commandBus,
										IGuidGetter guidGetter)
		{
			this.guidGetter = guidGetter;
			this.commandBus = commandBus;
			this.treeNodeProviderContext = treeNodeProviderContext;
			this.treeNodeRepository = treeNodeRepository;
		}

		public string Create(string parentNodeId, string providerTypeAssemblyQualifiedName)
		{
			ThrowExceptionIfTheProviderTypeDoesNotImplementIAmATreeNodeExtensionProvider(Type.GetType(providerTypeAssemblyQualifiedName));

			//var newTreeNode = treeNodeRepository.Create(new TreeNode()
			//                                                    {
			//                                                        Id = Guid.NewGuid().ToString(),
			//                                                        Type = providerTypeAssemblyQualifiedName,
			//                                                        ParentTreeNodeId = parentNodeId,
			//                                                    });

			var guid = guidGetter.GetGuid();
			commandBus.Send(new CreateTreeNodeCommand()
			                	{
			                		ParentId = parentNodeId,
									Type = Type.GetType(providerTypeAssemblyQualifiedName),
									TreeNodeId = guid,
									AggregateRootId = guid
			                	});

			return guid.ToString();
		}

		public void Delete(string nodeId)
		{
			throw new NotImplementedException();
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

		public TreeNodeSummary GetTreeNodeSummaryByTreeNodeId(string nodeId)
		{
			var treeNode = treeNodeRepository.GetAll().Where(a => a.Id == nodeId).FirstOrDefault();
			if (treeNode == null) return null;
			if (treeNode.Id == Constants.RootNodeId) return null;
			
			return GetTreeNodeSummaryForTreeNode(treeNode);
		}

		private TreeNodeSummary GetTreeNodeSummaryForTreeNode(TreeNode treeNode)
		{
			var provider = treeNodeProviderContext.GetProviderByTypeName(treeNode.Type);
			if (provider == null) throw new Exception(string.Format("Content tree node provider for type: {0} not found.", treeNode.Type));

			var treeNodeExtension = provider.GetAll().Where(a => a.TreeNodeId == treeNode.Id).FirstOrDefault();
			if (treeNodeExtension == null) throw new Exception(string.Format("Node with id \"{0}\" was not found by provider type \"{1}\".", treeNode.Id, provider.GetType().AssemblyQualifiedName));

			var treeNodeSummary = new TreeNodeSummary()
			       	{
						Name = treeNodeExtension.Name,
						Id = treeNode.Id,
						UrlSegment = treeNodeExtension.UrlSegment,
						HasChildren = treeNodeRepository.GetAll().Where(a => a.ParentTreeNodeId == treeNode.Id).Any(),
						ControllerToUseForModification = provider.ControllerToUseForModification,
						ActionToUseForModification = provider.ActionToUseForModification,
						ControllerToUseForCreation = provider.ControllerToUseForCreation,
						ActionToUseForCreation = provider.ActionToUseForCreation,
						RouteValuesForModification = new { TreeNodeId = treeNode.Id },
						RouteValuesForCreation = new { ParentTreeNodeId = treeNode.Id },
						ParentTreeNodeId = treeNode.ParentTreeNodeId,
						Sequence = treeNodeExtension.Sequence,
						Type = treeNode.Type,
			       	};
			return treeNodeSummary;
		}
	}
}