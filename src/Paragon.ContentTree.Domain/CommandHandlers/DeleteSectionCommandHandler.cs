using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Domain.AggregateRoots;
using Paragon.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.CommandHandlers
{
	public class DeleteSectionCommandHandler : AggregateRootCommandHandler<DeleteSectionCommand, Section>
	{
		public override void Handle(DeleteSectionCommand command, Section aggregateRoot)
		{
			aggregateRoot.Delete();
		}
	}
}
