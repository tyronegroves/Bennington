using System.Collections.Generic;
using System.Linq;
using Bennington.ContentTree.Denormalizers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;
using Bennington.Core.SisoDb;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Repositories
{
	public interface IContentNodeProviderDraftRepository
	{
		IList<ContentNodeProviderDraft> GetAllContentNodeProviderDrafts();
		void Update(ContentNodeProviderDraft instance);
		void Create(ContentNodeProviderDraft instance);
		void Delete(ContentNodeProviderDraft instance);
	}

	public class ContentNodeProviderDraftRepository : DatabaseFactory, IContentNodeProviderDraftRepository
	{
		private readonly IDataModelDataContext dataModelDataContext;

		public ContentNodeProviderDraftRepository(IDataModelDataContext dataModelDataContext)
		{
			this.dataModelDataContext = dataModelDataContext;
		}

		public IList<ContentNodeProviderDraft> GetAllContentNodeProviderDrafts()
		{
            using (var unitOfWork = database.CreateUnitOfWork())
            {
                return unitOfWork.GetAll<ContentNodeProviderDraft>().ToList();
            }
		}

		public void Delete(ContentNodeProviderDraft instance)
		{
			dataModelDataContext.Delete(instance);
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