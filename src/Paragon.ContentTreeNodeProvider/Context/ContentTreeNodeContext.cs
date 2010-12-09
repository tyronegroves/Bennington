using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTreeNodeProvider.Data;
using Paragon.ContentTreeNodeProvider.Mappers;
using Paragon.ContentTreeNodeProvider.Models;
using Paragon.ContentTreeNodeProvider.Repositories;

namespace Paragon.ContentTreeNodeProvider.Context
{
	public interface IContentTreeNodeContext
	{
		string CreateTreeNodeAndReturnTreeNodeId(ContentTreeNodeInputModel contentTreeNodeInputModel);
		void Delete(string id);
		IEnumerable<ContentTreeNode> GetContentTreeNodesByTreeId(string nodeId);
	}

	public class ContentTreeNodeContext : IContentTreeNodeContext
	{
		public const string RootNodeId = "0";
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
