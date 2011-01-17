using System.Linq;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.Repositories;

namespace Paragon.ContentTree.ContentNodeProvider.Repositories
{
	public interface IContentNodeProviderDraftRepository
	{
		IQueryable<ContentNodeProviderDraft> GetAllContentNodeProviderDrafts();
		void Update(ContentNodeProviderDraft instance);
		void Create(ContentNodeProviderDraft instance);
		void Delete(ContentNodeProviderDraft instance);
	}

	public class ContentNodeProviderDraftRepository : IContentNodeProviderDraftRepository
	{
		private readonly IDataModelDataContext dataModelDataContext;
		private readonly ITreeNodeRepository treeNodeRepository;

		public ContentNodeProviderDraftRepository(IDataModelDataContext dataModelDataContext, ITreeNodeRepository treeNodeRepository)
		{
			this.treeNodeRepository = treeNodeRepository;
			this.dataModelDataContext = dataModelDataContext;
		}

		public IQueryable<ContentNodeProviderDraft> GetAllContentNodeProviderDrafts()
		{
			return dataModelDataContext.ContentNodeProviderDrafts;
		}

		public void Delete(ContentNodeProviderDraft instance)
		{
			treeNodeRepository.Delete(instance.TreeNodeId);
		}

		public void Update(ContentNodeProviderDraft instance)
		{
			dataModelDataContext.Update(instance);
		}

		public void Create(ContentNodeProviderDraft instance)
		{
			dataModelDataContext.Create(instance);
		}
	}
}