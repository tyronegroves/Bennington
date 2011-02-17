using System;
using Bennington.ContentTree.Domain.AggregateRoots;
using Bennington.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Bennington.ContentTree.Domain.CommandHandlers
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
			var section = new Section(new Guid(command.SectionId));
			section.SetTreeNodeId(new Guid(command.TreeNodeId)); ;
			section.SetName(command.Name);
			section.SetParentTreeNodeId(new Guid(command.ParentTreeNodeId));
			if (!string.IsNullOrEmpty(command.DefaultTreeNodeId))
				section.SetDefaultTreeNodeId(new Guid(command.DefaultTreeNodeId));
			section.SetSequence(command.Sequence);
			section.SetUrlSegment(command.UrlSegment);
			section.SetInactive(command.Inactive);
			section.SetHidden(command.Hidden);
			domainRepository.Save(section);			
		}
	}
}
