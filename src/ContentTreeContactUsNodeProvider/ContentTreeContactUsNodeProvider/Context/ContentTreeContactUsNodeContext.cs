using System.Linq;
using Paragon.ContentTree.Contexts;
using Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Mappers;
using Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Models;
using Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Repositories;

namespace Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider.Context
{
	public interface IContentTreeContactUsNodeContext
	{
		string CreateTreeNodeAndReturnTreeNodeId(ContentTreeContactUsNodeInputModel contentTreeContactUsNodeInputModel);
		void Delete(string id);
	}

	public class ContentTreeContactUsNodeContext : IContentTreeContactUsNodeContext
	{
		private readonly IContentTreeContactUsNodeRepository contentTreeContactUsNodeRepository;
		private readonly IContentTreeContactUsInputModelToContentTreeContactUsNodeMapper contentTreeContactUsNodeInputModelToContentTreeContactUsNodeMapper;
		private readonly ITreeNodeSummaryContext treeNodeSummaryContext;

		public ContentTreeContactUsNodeContext(IContentTreeContactUsNodeRepository contentTreeContactUsNodeRepository, IContentTreeContactUsInputModelToContentTreeContactUsNodeMapper contentTreeContactUsNodeInputModelToContentTreeContactUsNodeMapper, ITreeNodeSummaryContext treeNodeSummaryContext)
		{
			this.treeNodeSummaryContext = treeNodeSummaryContext;
			this.contentTreeContactUsNodeInputModelToContentTreeContactUsNodeMapper = contentTreeContactUsNodeInputModelToContentTreeContactUsNodeMapper;
			this.contentTreeContactUsNodeRepository = contentTreeContactUsNodeRepository;
		}

		public string CreateTreeNodeAndReturnTreeNodeId(ContentTreeContactUsNodeInputModel contentTreeContactUsNodeInputModel)
		{
			var newTreeNodeId = treeNodeSummaryContext.Create(contentTreeContactUsNodeInputModel.ParentTreeNodeId, typeof(ContentTreeContactUsNodeProvider));
			contentTreeContactUsNodeInputModel.TreeNodeId = newTreeNodeId;
			var node = contentTreeContactUsNodeInputModelToContentTreeContactUsNodeMapper.CreateInstance(contentTreeContactUsNodeInputModel);
			contentTreeContactUsNodeRepository.Create(node);
			return contentTreeContactUsNodeInputModel.TreeNodeId;
		}

		public void Delete(string id)
		{
			var node = contentTreeContactUsNodeRepository.GetAll().Where(a => a.TreeNodeId == id).FirstOrDefault();
			if (node != null)
			    contentTreeContactUsNodeRepository.Delete(node);
		}
	}
}
