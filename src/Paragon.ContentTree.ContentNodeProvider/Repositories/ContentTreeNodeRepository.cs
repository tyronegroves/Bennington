using System;
using System.Linq;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Repositories;
using IDataModelDataContext = Paragon.ContentTree.ContentNodeProvider.Data.IDataModelDataContext;

namespace Paragon.ContentTree.ContentNodeProvider.Repositories
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
			throw new NotImplementedException();
			//return dataModelDataContext.ContentTreeNodes;
		}

		public void Delete(ContentTreeNode instance)
		{
			treeNodeRepository.Delete(instance.TreeNodeId);
		}

		public void Update(ContentTreeNode instance)
		{
			throw new NotImplementedException();
			//dataModelDataContext.Update(instance);
		}

		public void Create(ContentTreeNode instance)
		{
			throw new NotImplementedException();
			//dataModelDataContext.Create(instance);
		}
	}
}