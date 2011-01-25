using System;
using System.Linq;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.Data;
using Paragon.ContentTree.Repositories;
using IDataModelDataContext = Paragon.ContentTree.ContentNodeProvider.Data.IDataModelDataContext;

namespace Paragon.ContentTree.ContentNodeProvider.Repositories
{
	public interface IContentTreeNodeRepository
	{
		IQueryable<ContentTreeNode> GetAllContentTreeNodes();
	}

	public class ContentTreeNodeRepository : IContentTreeNodeRepository
	{
		private readonly IContentNodeProviderDraftRepository contentNodeProviderDraftRepository;
		private readonly IContentNodeProviderDraftToContentTreeNodeMapper contentNodeProviderDraftToContentTreeNodeMapper;

		public ContentTreeNodeRepository(IContentNodeProviderDraftRepository contentNodeProviderDraftRepository,
										IContentNodeProviderDraftToContentTreeNodeMapper contentNodeProviderDraftToContentTreeNodeMapper)
		{
			this.contentNodeProviderDraftToContentTreeNodeMapper = contentNodeProviderDraftToContentTreeNodeMapper;
			this.contentNodeProviderDraftRepository = contentNodeProviderDraftRepository;
		}

		public IQueryable<ContentTreeNode> GetAllContentTreeNodes()
		{
			return contentNodeProviderDraftToContentTreeNodeMapper
				.CreateSet(contentNodeProviderDraftRepository.GetAllContentNodeProviderDrafts()).AsQueryable();
		}
	}
}