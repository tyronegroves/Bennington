using System.Linq;
using Bennington.ContentTree.Domain.Events.Page;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Repositories;
using SimpleCqrs.Eventing;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Denormalizers
{
	public class ContentNodeProviderPublishDenormalizer : IHandleDomainEvents<PagePublishedEvent>,
															IHandleDomainEvents<PageDeletedEvent>
	{
		private readonly IContentNodeProviderPublishedVersionRepository contentNodeProviderPublishedVersionRepository;
		private readonly IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper;
		private readonly IContentNodeProviderDraftRepository contentNodeProviderDraftRepository;

		public ContentNodeProviderPublishDenormalizer(IContentNodeProviderPublishedVersionRepository contentNodeProviderPublishedVersionRepository,
			IContentNodeProviderDraftToContentNodeProviderPublishedVersionMapper contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper,
			IContentNodeProviderDraftRepository contentNodeProviderDraftRepository)
		{
			this.contentNodeProviderDraftRepository = contentNodeProviderDraftRepository;
			this.contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper = contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper;
			this.contentNodeProviderPublishedVersionRepository = contentNodeProviderPublishedVersionRepository;
		}

		public void Handle(PagePublishedEvent domainEvent)
		{
			var draftVersion = contentNodeProviderDraftRepository.GetAllContentNodeProviderDrafts().Where(a => a.PageId == domainEvent.AggregateRootId.ToString()).FirstOrDefault();

			var existingPublishedVersion = contentNodeProviderPublishedVersionRepository.GetAllContentNodeProviderPublishedVersions()
				.Where(a => a.PageId == domainEvent.AggregateRootId.ToString()).FirstOrDefault();

			if (existingPublishedVersion == null)
				contentNodeProviderPublishedVersionRepository
					.Create(contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper.CreateInstance(draftVersion));
			else
			{
				var mappedInstance = contentNodeProviderDraftToContentNodeProviderPublishedVersionMapper.CreateInstance(draftVersion);
				contentNodeProviderPublishedVersionRepository.Update(mappedInstance);
			}
		}

		public void Handle(PageDeletedEvent domainEvent)
		{
			var item = contentNodeProviderPublishedVersionRepository
							.GetAllContentNodeProviderPublishedVersions()
								.Where(a => a.TreeNodeId == domainEvent.TreeNodeId.ToString()).FirstOrDefault();

			if (item != null)
				contentNodeProviderPublishedVersionRepository.Delete(item);
		}
	}
}
