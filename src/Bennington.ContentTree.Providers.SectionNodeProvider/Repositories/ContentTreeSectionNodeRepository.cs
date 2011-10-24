using System.Collections.Generic;
using System.Linq;
using Bennington.ContentTree.Denormalizers;
using Bennington.ContentTree.Providers.SectionNodeProvider.Data;
using Bennington.ContentTree.Providers.SectionNodeProvider.Mappers;
using Bennington.ContentTree.Providers.SectionNodeProvider.Models;
using Bennington.ContentTree.Repositories;
using Bennington.Core.SisoDb;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Repositories
{
	public interface IContentTreeSectionNodeRepository
	{
		IList<ContentTreeSectionNode> GetAllContentTreeSectionNodes();
		void Delete(ContentTreeSectionNode instance);
	}

    public class ContentTreeSectionNodeRepository : DatabaseFactory, IContentTreeSectionNodeRepository
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

		public IList<ContentTreeSectionNode> GetAllContentTreeSectionNodes()
		{
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                return unitOfWork.GetAll<ContentTreeSectionNode>().ToList();
            }
			//return dataModelDataContext.ContentTreeSectionNodes;
		}

		public void Delete(ContentTreeSectionNode instance)
		{
			// dataModelDataContext.Delete(instance);
			treeNodeRepository.Delete(instance.TreeNodeId);
		}
	}
}