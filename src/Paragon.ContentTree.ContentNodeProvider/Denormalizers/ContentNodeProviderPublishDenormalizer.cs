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
	public class ContentNodeProviderPublishDenormalizer : IHandleDomainEvents<PagePublishedEvent>
	{
		private IContentNodeProviderPublishedVersionRepository contentNodeProviderPublishedVersionRepository;

		public ContentNodeProviderPublishDenormalizer(IContentNodeProviderPublishedVersionRepository contentNodeProviderPublishedVersionRepository)
		{
			this.contentNodeProviderPublishedVersionRepository = contentNodeProviderPublishedVersionRepository;
		}

		public void Handle(PagePublishedEvent domainEvent)
		{
			//throw new NotImplementedException();
			var x = 1;
		}
	}
}
