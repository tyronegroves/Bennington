using System;
using System.Linq;
using Paragon.ContentTree.Repositories;
using Paragon.ContentTreeSectionNodeProvider.Data;


namespace Paragon.ContentTreeSectionNodeProvider.Repositories
{
	public interface IContentTreeSectionNodeRepository
	{
		IQueryable<ContentTreeSectionNode> GetAllContentTreeSectionNodes();
		void Update(ContentTreeSectionNode instance);
		void Create(ContentTreeSectionNode instance);
		void Delete(ContentTreeSectionNode instance);
	}

	public class ContentTreeSectionNodeRepository : IContentTreeSectionNodeRepository
	{
		private readonly IDataModelDataContext dataModelDataContext;
		private readonly ITreeNodeRepository treeNodeRepository;

		public ContentTreeSectionNodeRepository(IDataModelDataContext dataModelDataContext, ITreeNodeRepository treeNodeRepository)
		{
			this.treeNodeRepository = treeNodeRepository;
			this.dataModelDataContext = dataModelDataContext;
		}

		public IQueryable<ContentTreeSectionNode> GetAllContentTreeSectionNodes()
		{
			return dataModelDataContext.ContentTreeSectionNodes;
		}

		public void Delete(ContentTreeSectionNode instance)
		{
			
			// dataModelDataContext.Delete(instance);
			treeNodeRepository.Delete(instance.TreeNodeId);
		}

		public void Update(ContentTreeSectionNode instance)
		{
			dataModelDataContext.Update(instance);
		}

		public void Create(ContentTreeSectionNode instance)
		{
			dataModelDataContext.Create(instance);
		}
	}
}