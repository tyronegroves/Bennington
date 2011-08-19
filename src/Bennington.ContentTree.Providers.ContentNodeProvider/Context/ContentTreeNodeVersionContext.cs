using System.Linq;
using Bennington.ContentTree.Contexts;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Context
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
			{
			    var contentNodeProviderPublishedVersions = contentNodeProviderPublishedVersionRepository.GetAllContentNodeProviderPublishedVersions().Where(a => a.Inactive == false);
				return contentNodeProviderPublishedVersionToContentTreeNodeMapper.CreateSet(contentNodeProviderPublishedVersions).AsQueryable();
			}
				
			if (versionContext.GetCurrentVersionId() == VersionContext.Manage)
			{
				return contentNodeProviderDraftToContentTreeNodeMapper
						.CreateSet(contentNodeProviderDraftRepository.GetAllContentNodeProviderDrafts()).AsQueryable();
			}

			return contentNodeProviderDraftToContentTreeNodeMapper
					.CreateSet(contentNodeProviderDraftRepository.GetAllContentNodeProviderDrafts().Where(a => a.Inactive == false)).AsQueryable();
		}
	}
}