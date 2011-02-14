using System.Linq;
using Bennington.ContentTree.Providers.ContentNodeProvider.Data;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Repositories
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

		public ContentNodeProviderDraftRepository(IDataModelDataContext dataModelDataContext)
		{
			this.dataModelDataContext = dataModelDataContext;
		}

		public IQueryable<ContentNodeProviderDraft> GetAllContentNodeProviderDrafts()
		{
			var x = dataModelDataContext.ContentNodeProviderDrafts.ToArray();
			return x.AsQueryable();
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