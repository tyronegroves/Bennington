using Bennington.ContentTree.Domain.AggregateRoots;
using Bennington.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.CommandHandlers
{
	public class DeletePageCommandHandler : AggregateRootCommandHandler<DeletePageCommand, Page>
	{
		public override void Handle(DeletePageCommand command, Page aggregateRoot)
		{
            aggregateRoot.SetLastModifyBy(command.LastModifyBy);
			aggregateRoot.Delete(command.AggregateRootId);
		}
	}
}
