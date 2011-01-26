using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Mappers;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Domain.Events.Page;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.ContentNodeProvider.Denormalizers
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
