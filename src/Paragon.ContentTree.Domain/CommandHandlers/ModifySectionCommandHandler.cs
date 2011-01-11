using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Domain.AggregateRoots;
using Paragon.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.CommandHandlers
{
	public class ModifySectionCommandHandler : AggregateRootCommandHandler<ModifySectionCommand, Section>
	{
		public override void Handle(ModifySectionCommand command, Section page)
		{
			page.SetParentTreeNodeId(new Guid(command.ParentTreeNodeId));
			page.SetName(command.Name);
			page.SetDefaultPage(new Guid(command.DefaultTreeNodeId));
			page.SetSequence(command.Sequence);
			page.SetUrlSegment(command.UrlSegment);
		}
	}
}
