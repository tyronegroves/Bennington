using System;
using System.Collections.Generic;
using System.Linq;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Contexts;

namespace Paragon.ContentTree.ContentNodeProvider.Context
{
	public interface IContentTreeNodeContext
	{
		string CreateTreeNodeAndReturnTreeNodeId(ContentTreeNodeInputModel contentTreeNodeInputModel);
		void Delete(string id);
		IEnumerable<ContentTreeNode> GetContentTreeNodesByTreeId(string nodeId);
	}

	public class ContentTreeNodeContext : IContentTreeNodeContext
	{
		public const string RootNodeId = "00000000-0000-0000-0000-000000000000";
		private readonly IContentTreeNodeRepository contentTreeNodeRepository;
		private readonly IContentTreeNodeInputModelToContentTreeNodeMapper contentTreeNodeInputModelToContentTreeNodeMapper;
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

		public ContentTreeNodeContext(IContentTreeNodeRepository contentTreeNodeRepository, IContentTreeNodeInputModelToContentTreeNodeMapper contentTreeNodeInputModelToContentTreeNodeMapper, ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
			this.contentTreeNodeInputModelToContentTreeNodeMapper = contentTreeNodeInputModelToContentTreeNodeMapper;
			this.contentTreeNodeRepository = contentTreeNodeRepository;
		}

		public string CreateTreeNodeAndReturnTreeNodeId(ContentTreeNodeInputModel contentTreeNodeInputModel)
		{
			var newTreeNodeId = treeNodeSummaryContext.Create(contentTreeNodeInputModel.ParentTreeNodeId, contentTreeNodeInputModel.Type);
			contentTreeNodeInputModel.TreeNodeId = newTreeNodeId;
			var node = contentTreeNodeInputModelToContentTreeNodeMapper.CreateInstance(contentTreeNodeInputModel);
			contentTreeNodeRepository.Create(node);
			return contentTreeNodeInputModel.TreeNodeId;
		}

		public void Delete(string id)
		{
			var node = contentTreeNodeRepository.GetAllContentTreeNodes().Where(a => a.TreeNodeId == id).FirstOrDefault();
			if (node != null)
				contentTreeNodeRepository.Delete(node);
		}

		public IEnumerable<ContentTreeNode> GetContentTreeNodesByTreeId(string treeNodeId)
		{
			var contentTreeNodes = contentTreeNodeRepository.GetAllContentTreeNodes().Where(a => a.TreeNodeId == treeNodeId);
			return contentTreeNodes.ToArray();
		}
	}
}
