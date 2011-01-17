using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Domain.AggregateRoots;
using Paragon.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.CommandHandlers
{
	public class DeletePageCommandHandler : AggregateRootCommandHandler<DeletePageCommand, Page>
	{
		public override void Handle(DeletePageCommand command, Page aggregateRoot)
		{
			aggregateRoot.Delete(command.AggregateRootId);
		}
	}
}
