using System;
using System.Linq;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.Repositories;

namespace Paragon.ContentTree.ContentNodeProvider.Repositories
{
	public interface IContentNodeProviderPublishedVersionRepository
	{
		IQueryable<ContentNodeProviderPublishedVersion> GetAllContentNodeProviderPublishedVersions();
		void Update(ContentNodeProviderPublishedVersion instance);
		void Create(ContentNodeProviderPublishedVersion instance);
		void Delete(ContentNodeProviderPublishedVersion instance);
	}

	public class ContentNodeProviderPublishedVersionRepository : IContentNodeProviderPublishedVersionRepository
	{
		private readonly IDataModelDataContext dataModelDataContext;

		public ContentNodeProviderPublishedVersionRepository(IDataModelDataContext dataModelDataContext)
		{
			this.dataModelDataContext = dataModelDataContext;
		}

		public IQueryable<ContentNodeProviderPublishedVersion> GetAllContentNodeProviderPublishedVersions()
		{
			return dataModelDataContext.ContentNodeProviderPublishedVersions;
		}

		public void Update(ContentNodeProviderPublishedVersion instance)
		{
			throw new NotImplementedException();
		}

		public void Create(ContentNodeProviderPublishedVersion instance)
		{
			throw new NotImplementedException();
		}

		public void Delete(ContentNodeProviderPublishedVersion instance)
		{
			throw new NotImplementedException();
		}
	}
}