using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Domain.AggregateRoots;
using Paragon.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Paragon.ContentTree.Domain.CommandHandlers
{
	public class PublishPageCommandHandler : AggregateRootCommandHandler<PublishPageCommand, Page>
	{
		private readonly IDomainRepository domainRepository;

		public PublishPageCommandHandler(IDomainRepository domainRepository)
		{
			this.domainRepository = domainRepository;
		}

		public override void Handle(PublishPageCommand command, Page aggregateRoot)
		{
			var page = new Page();
			page.PageId = command.AggregateRootId;
			page.Publish();
			domainRepository.Save(page);
		}
	}
}
