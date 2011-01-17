using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.ContentNodeProvider.Data;
using Paragon.ContentTree.ContentNodeProvider.Repositories;
using Paragon.ContentTree.Domain.Events.Page;
using SimpleCqrs.Eventing;

namespace Paragon.ContentTree.ContentNodeProvider.Denormalizers
{
	public class ContentNodeProviderDraftDenormalizer : IHandleDomainEvents<PageCreatedEvent>
	{
		private readonly IContentNodeProviderDraftRepository contentNodeProviderDraftRepository;

		public ContentNodeProviderDraftDenormalizer(IContentNodeProviderDraftRepository contentNodeProviderDraftRepository)
		{
			this.contentNodeProviderDraftRepository = contentNodeProviderDraftRepository;
		}

		public void Handle(PageCreatedEvent domainEvent)
		{
			contentNodeProviderDraftRepository.Create(new ContentNodeProviderDraft()
			                                          	{
			                                          		TreeNodeId = domainEvent.AggregateRootId.ToString()
			                                          	});
		}
	}
}