using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.ContentTree.ToolLinkNodeProvider.Data;
using Paragon.Core.EntityFramework;

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
			var toolLinkProviderDataModel = new ToolLinkProviderDataModel(entityConnectionInformation.GetEntityConnectionString("Data.ToolLinkProviderDataModel"));

			return toolLinkProviderDataModel.ToolLinkProviderDrafts;
		}
	}
}