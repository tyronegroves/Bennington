using System;
using System.Collections.Generic;
using System.Linq;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Routing.Content;
using Paragon.ContentTree.Routing.Data;
using Paragon.ContentTree.Routing.Mappers;

namespace Paragon.ContentTree.Routing.Adapters
{
	public class TreeNodeSummaryContextToContentTreeRepositoryAdapter : IContentTreeRepository
	{
		public const string RootNodeId = "";
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;
		private readonly ITreeNodeSummaryToContentTreeNodeMapper treeNodeSummaryToContentTreeNodeMapper;

		public TreeNodeSummaryContextToContentTreeRepositoryAdapter(ITreeNodeSummaryContext treeNodeSummaryContext, ITreeNodeSummaryToContentTreeNodeMapper treeNodeSummaryToContentTreeNodeMapper)
		{
			this.treeNodeSummaryToContentTreeNodeMapper = treeNodeSummaryToContentTreeNodeMapper;
			this.treeNodeSummaryContext = treeNodeSummaryContext;
		}

		public ContentTreeNode GetRootNode()
		{
			var children = treeNodeSummaryContext.GetChildren(RootNodeId);
			var rootSummary = children.FirstOrDefault();
			if (rootSummary == null) throw new Exception("Root node was not found.");

			return treeNodeSummaryToContentTreeNodeMapper.CreateInstance(rootSummary);
		}

		public IEnumerable<ContentTreeNode> GetChildNodesForNode(ContentTreeNode node)
		{
			var contentTreeNodes = new List<ContentTreeNode>();
			
			var childrenSummaries = treeNodeSummaryContext.GetChildren(node.NodeId);
			foreach (var childSummary in childrenSummaries)
			{
				contentTreeNodes.Add(treeNodeSummaryToContentTreeNodeMapper.CreateInstance(childSummary));
			}
			
			return contentTreeNodes;
		}
	}
}