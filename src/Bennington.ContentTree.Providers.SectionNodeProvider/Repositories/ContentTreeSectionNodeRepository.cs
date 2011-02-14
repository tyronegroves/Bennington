using System.Linq;
using Bennington.ContentTree.Providers.SectionNodeProvider.Data;
using Bennington.ContentTree.Providers.SectionNodeProvider.Mappers;
using Bennington.ContentTree.Providers.SectionNodeProvider.Models;
using Bennington.ContentTree.Repositories;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Repositories
{
	public interface IContentTreeSectionNodeRepository
	{
		IQueryable<ContentTreeSectionNode> GetAllContentTreeSectionNodes();
		void Delete(ContentTreeSectionNode instance);
	}

	public class ContentTreeSectionNodeRepository : IContentTreeSectionNodeRepository
	{
		private readonly IDataModelDataContext dataModelDataContext;
		private readonly ITreeNodeRepository treeNodeRepository;
		private readonly ISectionNodeProviderDraftToContentTreeSectionNodeMapper sectionNodeProviderDraftToContentTreeSectionNodeMapper;

		public ContentTreeSectionNodeRepository(IDataModelDataContext dataModelDataContext, ITreeNodeRepository treeNodeRepository, ISectionNodeProviderDraftToContentTreeSectionNodeMapper sectionNodeProviderDraftToContentTreeSectionNodeMapper)
		{
			this.sectionNodeProviderDraftToContentTreeSectionNodeMapper = sectionNodeProviderDraftToContentTreeSectionNodeMapper;
			this.treeNodeRepository = treeNodeRepository;
			this.dataModelDataContext = dataModelDataContext;
		}

		public IQueryable<ContentTreeSectionNode> GetAllContentTreeSectionNodes()
		{
			return sectionNodeProviderDraftToContentTreeSectionNodeMapper.CreateSet(dataModelDataContext.GetAllSectionNodeProviderDrafts()).AsQueryable();
			//return dataModelDataContext.ContentTreeSectionNodes;
		}

		public void Delete(ContentTreeSectionNode instance)
		{
			// dataModelDataContext.Delete(instance);
			treeNodeRepository.Delete(instance.TreeNodeId);
		}
	}
}