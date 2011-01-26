using System;
using System.Linq;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Contexts;

namespace Paragon.ContentTree.ContentNodeProvider.Context
{
	public interface IContentTreeNodeVersionContext
	{
		IQueryable<ContentTreeNode> GetAllContentTreeNodes();
	}

	public class ContentTreeNodeVersionContext : IContentTreeNodeVersionContext
	{
		private readonly IContentNodeProviderDraftRepository contentNodeProviderDraftRepository;
		private readonly IContentNodeProviderDraftToContentTreeNodeMapper contentNodeProviderDraftToContentTreeNodeMapper;
		private readonly IVersionContext versionContext;
		private readonly IContentNodeProviderPublishedVersionToContentTreeNodeMapper contentNodeProviderPublishedVersionToContentTreeNodeMapper;
		private readonly IContentNodeProviderPublishedVersionRepository contentNodeProviderPublishedVersionRepository;

		public ContentTreeNodeVersionContext(IContentNodeProviderDraftRepository contentNodeProviderDraftRepository,
										IContentNodeProviderDraftToContentTreeNodeMapper contentNodeProviderDraftToContentTreeNodeMapper,
										IVersionContext versionContext,
										IContentNodeProviderPublishedVersionToContentTreeNodeMapper contentNodeProviderPublishedVersionToContentTreeNodeMapper,
										IContentNodeProviderPublishedVersionRepository contentNodeProviderPublishedVersionRepository)
		{
			this.contentNodeProviderPublishedVersionRepository = contentNodeProviderPublishedVersionRepository;
			this.contentNodeProviderPublishedVersionToContentTreeNodeMapper = contentNodeProviderPublishedVersionToContentTreeNodeMapper;
			this.versionContext = versionContext;
			this.contentNodeProviderDraftToContentTreeNodeMapper = contentNodeProviderDraftToContentTreeNodeMapper;
			this.contentNodeProviderDraftRepository = contentNodeProviderDraftRepository;
		}

		public IQueryable<ContentTreeNode> GetAllContentTreeNodes()
		{
			if (versionContext.GetCurrentVersionId() == VersionContext.Publish)
				return contentNodeProviderPublishedVersionToContentTreeNodeMapper.CreateSet(contentNodeProviderPublishedVersionRepository.GetAllContentNodeProviderPublishedVersions()).AsQueryable();

			return contentNodeProviderDraftToContentTreeNodeMapper
					.CreateSet(contentNodeProviderDraftRepository.GetAllContentNodeProviderDrafts()).AsQueryable();
		}
	}
}