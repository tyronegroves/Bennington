using System.Linq;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.SectionNodeProvider.Mappers;
using Paragon.ContentTree.SectionNodeProvider.Models;
using Paragon.ContentTree.SectionNodeProvider.Repositories;

namespace Paragon.ContentTree.SectionNodeProvider.Context
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
			var newTreeNodeId = treeNodeSummaryContext.Create(contentTreeSectionInputModel.ParentTreeNodeId, typeof(SectionNodeProvider).AssemblyQualifiedName);
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
