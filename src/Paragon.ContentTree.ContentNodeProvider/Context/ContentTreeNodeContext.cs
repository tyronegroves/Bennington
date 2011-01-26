using System;
using System.Collections.Generic;
using System.Linq;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Data;

namespace Paragon.ContentTree.ContentNodeProvider.Context
{
	public interface IContentTreeNodeContext
	{
		string CreateTreeNodeAndReturnTreeNodeId(ContentTreeNodeInputModel contentTreeNodeInputModel);
		IEnumerable<ContentTreeNode> GetContentTreeNodesByTreeId(string nodeId);
	}

	public class ContentTreeNodeContext : IContentTreeNodeContext
	{
		public const string RootNodeId = Constants.RootNodeId;
		private readonly IContentTreeNodeVersionContext contentTreeNodeVersionContext;
		private readonly IContentTreeNodeInputModelToContentTreeNodeMapper contentTreeNodeInputModelToContentTreeNodeMapper;
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

		public ContentTreeNodeContext(IContentTreeNodeVersionContext contentTreeNodeVersionContext, IContentTreeNodeInputModelToContentTreeNodeMapper contentTreeNodeInputModelToContentTreeNodeMapper, ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
			this.contentTreeNodeInputModelToContentTreeNodeMapper = contentTreeNodeInputModelToContentTreeNodeMapper;
			this.contentTreeNodeVersionContext = contentTreeNodeVersionContext;
		}

		public string CreateTreeNodeAndReturnTreeNodeId(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			var newTreeNodeId = treeNodeSummaryContext.Create(contentTreeNodeInputModel.ParentTreeNodeId, contentTreeNodeInputModel.Type);
			contentTreeNodeInputModel.TreeNodeId = newTreeNodeId;
			return contentTreeNodeInputModel.TreeNodeId;
		}

		public IEnumerable<ContentTreeNode> GetContentTreeNodesByTreeId(string treeNodeId)
		{
			var contentTreeNodes = contentTreeNodeVersionContext.GetAllContentTreeNodes().Where(a => a.TreeNodeId == treeNodeId);
			return contentTreeNodes.ToArray();
		}
	}
}
