using Bennington.ContentTree.Domain.AggregateRoots;
using Bennington.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.CommandHandlers
{
	public class DeleteSectionCommandHandler : AggregateRootCommandHandler<DeleteSectionCommand, Section>
	{
		public override void Handle(DeleteSectionCommand command, Section aggregateRoot)
		{
			aggregateRoot.SectionId = command.AggregateRootId;
			aggregateRoot.Delete();
		}
	}
}
