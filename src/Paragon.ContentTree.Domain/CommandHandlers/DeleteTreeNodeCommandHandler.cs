using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Domain.AggregateRoots;
using Paragon.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.CommandHandlers
{
	public class DeleteTreeNodeCommandHandler : AggregateRootCommandHandler<DeleteTreeNodeCommand, TreeNode>
	{
		public override void Handle(DeleteTreeNodeCommand command, TreeNode aggregateRoot)
		{
			aggregateRoot.Delete(command.AggregateRootId);
		}
	}
}
