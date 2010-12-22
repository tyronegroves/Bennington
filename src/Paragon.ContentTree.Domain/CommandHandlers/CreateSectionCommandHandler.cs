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
	public class CreateSectionCommandHandler : CommandHandler<CreateSectionCommand>
	{
		private readonly IDomainRepository domainRepository;

		public CreateSectionCommandHandler(IDomainRepository domainRepository)
		{
			this.domainRepository = domainRepository;
		}

		public override void Handle(CreateSectionCommand command)
		{
			var page = new Section(command.AggregateRootId);
			page.SetName(command.Name);
			page.SetParentId(command.ParentId);
			page.SetDefaultPage(command.DefaultPageId);
			page.SetSequence(command.Sequence);
			page.SetUrlSegment(command.UrlSegment);

			domainRepository.Save(page);			
		}
	}
}
