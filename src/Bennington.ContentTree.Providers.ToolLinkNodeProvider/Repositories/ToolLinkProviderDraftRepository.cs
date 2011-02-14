using System;
using System.Linq;
using Bennington.Core.EntityFramework;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Repositories
{
	public interface IToolLinkProviderDraftRepository
	{
		IQueryable<Data.ToolLinkProviderDraft> GetAll();
	}

	public class ToolLinkProviderDraftRepository : IToolLinkProviderDraftRepository
	{
		private readonly IEntityConnectionInformation entityConnectionInformation;

		public ToolLinkProviderDraftRepository(IEntityConnectionInformation entityConnectionInformation)
		{
			this.entityConnectionInformation = entityConnectionInformation;
		}

		public IQueryable<Data.ToolLinkProviderDraft> GetAll()
		{
			throw new NotImplementedException();
		}
	}
}