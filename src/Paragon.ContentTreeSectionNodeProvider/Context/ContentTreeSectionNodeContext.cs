using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTreeSectionNodeProvider.Mappers;
using Paragon.ContentTreeSectionNodeProvider.Models;
using Paragon.ContentTreeSectionNodeProvider.Repositories;

namespace Paragon.ContentTreeSectionNodeProvider.Context
{
	public interface IContentTreeSectionNodeContext
	{
		string CreateTreeNodeAndReturnTreeNodeId(ContentTreeSectionInputModel contentTreeSectionInputModel);
		void Delete(string id);
	}

	public class ContentTreeSectionNodeContext : IContentTreeSectionNodeContext
	{
		private readonly IContentTreeSectionNodeRepository contentTreeSectionNodeRepository;
		private readonly IContentTreeSectionInputModelToContentTreeSectionNodeMapper contentTreeSectionInputModelToContentTreeSectionNodeMapper;
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

		public ContentTreeSectionNodeContext(IContentTreeSectionNodeRepository contentTreeSectionNodeRepository, IContentTreeSectionInputModelToContentTreeSectionNodeMapper ContentTreeSectionInputModelToContentTreeSectionNodeMapper, ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
			this.contentTreeSectionInputModelToContentTreeSectionNodeMapper = ContentTreeSectionInputModelToContentTreeSectionNodeMapper;
			this.contentTreeSectionNodeRepository = contentTreeSectionNodeRepository;
		}

		public string CreateTreeNodeAndReturnTreeNodeId(ContentTreeSectionInputModel contentTreeSectionInputModel)
		{
			var newTreeNodeId = treeNodeSummaryContext.Create(contentTreeSectionInputModel.ParentTreeNodeId, typeof(ContentTreeSectionNodeExtensionProvider));
			contentTreeSectionInputModel.TreeNodeId = newTreeNodeId;
			var node = contentTreeSectionInputModelToContentTreeSectionNodeMapper.CreateInstance(contentTreeSectionInputModel);
			contentTreeSectionNodeRepository.Create(node);
			return contentTreeSectionInputModel.TreeNodeId;
		}

		public void Delete(string id)
		{
			var node = contentTreeSectionNodeRepository.GetAllContentTreeSectionNodes().Where(a => a.TreeNodeId == id).FirstOrDefault();
			if (node != null)
				contentTreeSectionNodeRepository.Delete(node);
		}
	}
}
