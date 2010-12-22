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
	public class CreatePageCommandHandler : CommandHandler<CreatePageCommand>
	{
		private readonly IDomainRepository domainRepository;

		public CreatePageCommandHandler(IDomainRepository domainRepository)
		{
			this.domainRepository = domainRepository;
		}

		public override void Handle(CreatePageCommand command)
		{
			var page = new Page(command.PageId);
			page.SetStepId("Index");
			page.SetParentId(command.ParentId);
			page.SetBody(command.Body);
			page.SetHeaderText(command.HeaderText);
			page.SetSequence(command.Sequence);
			page.SetUrlSegment(command.UrlSegment);
			page.SetType(command.Type);

			domainRepository.Save(page);
		}
	}
}
