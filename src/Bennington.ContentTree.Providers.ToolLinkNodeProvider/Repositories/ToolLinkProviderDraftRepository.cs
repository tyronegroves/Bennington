using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bennington.Core.EntityFramework;
using Paragon.ContentTree.ToolLinkNodeProvider.Data;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Repositories
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