using System;
using System.Linq;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTreeNodeProvider.Data;


namespace Paragon.ContentTreeNodeProvider.Repositories
{
	public interface IContentTreeNodeRepository
	{
		IQueryable<ContentTreeNode> GetAllContentTreeNodes();
		void Update(ContentTreeNode instance);
		void Create(ContentTreeNode instance);
		void Delete(ContentTreeNode instance);
	}

	public class ContentTreeNodeRepository : IContentTreeNodeRepository
	{
		private readonly IDataModelDataContext dataModelDataContext;
		private readonly ITreeNodeRepository treeNodeRepository;

		public ContentTreeNodeRepository(IDataModelDataContext dataModelDataContext, ITreeNodeRepository treeNodeRepository)
		{
			this.treeNodeRepository = treeNodeRepository;
			this.dataModelDataContext = dataModelDataContext;
		}

		public IQueryable<ContentTreeNode> GetAllContentTreeNodes()
		{
			return dataModelDataContext.ContentTreeNodes;
		}

		public void Delete(ContentTreeNode instance)
		{
			
			// dataModelDataContext.Delete(instance);
			treeNodeRepository.Delete(instance.TreeNodeId);
		}

		public void Update(ContentTreeNode instance)
		{
			dataModelDataContext.Update(instance);
		}

		public void Create(ContentTreeNode instance)
		{
			dataModelDataContext.Create(instance);
		}
	}
}