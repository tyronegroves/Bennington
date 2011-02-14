using Bennington.ContentTree.Domain.AggregateRoots;
using Bennington.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.CommandHandlers
{
	public class DeleteTreeNodeCommandHandler : AggregateRootCommandHandler<DeleteTreeNodeCommand, TreeNode>
	{
		public override void Handle(DeleteTreeNodeCommand command, TreeNode aggregateRoot)
		{
			aggregateRoot.Delete(command.AggregateRootId);
		}
	}
}
