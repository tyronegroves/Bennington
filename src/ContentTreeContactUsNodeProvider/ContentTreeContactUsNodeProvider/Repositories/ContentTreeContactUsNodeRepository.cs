using System;
using System.Configuration;
using System.Linq;
using ContentTreeContactUsNodeProvider.Data;
using Paragon.Core.EntityFramework;

namespace ContentTreeContactUsNodeProvider.Repositories
{
	public interface IContentTreeContactUsNodeRepository
	{
		IQueryable<ContentTreeContactUsNode> GetAll();
		void Delete(ContentTreeContactUsNode node);
		void Create(ContentTreeContactUsNode node);
		void Update(ContentTreeContactUsNode contentTreeContactUsNodeFromRepository);
	}

	public class ContentTreeContactUsNodeRepository : IContentTreeContactUsNodeRepository
	{
		private readonly IEntityConnectionInformation entityConnectionInformation;

		public ContentTreeContactUsNodeRepository(IEntityConnectionInformation entityConnectionInformation)
		{
			this.entityConnectionInformation = entityConnectionInformation;
		}

		public IQueryable<ContentTreeContactUsNode> GetAll()
		{
			var dataModel = new ContentTreecontactUsNodeDataModel(entityConnectionInformation.GetEntityConnectionString("Data.ContentTreeContactUsNodeProviderDataContext"));
			return dataModel.ContentTreeContactUsNodes;
		}

		public void Delete(ContentTreeContactUsNode node)
		{
			throw new NotImplementedException();
		}

		public void Create(ContentTreeContactUsNode node)
		{
			var dataModel = new ContentTreecontactUsNodeDataModel(entityConnectionInformation.GetEntityConnectionString("Data.ContentTreeContactUsNodeProviderDataContext"));
			dataModel.ContentTreeContactUsNodes.AddObject(node);
			dataModel.SaveChanges();
		}

		public void Update(ContentTreeContactUsNode contentTreeContactUsNodeFromRepository)
		{
			var dataModel = new ContentTreecontactUsNodeDataModel(entityConnectionInformation.GetEntityConnectionString("Data.ContentTreeContactUsNodeProviderDataContext"));
			dataModel.Attach(contentTreeContactUsNodeFromRepository);
			dataModel.SaveChanges();
		}
	}
}