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
		public override void Handle(ModifySectionCommand command, Section section)
		{
			section.SectionId = command.AggregateRootId;
			section.SetTreeNodeId(new Guid(command.TreeNodeId));
			if (!string.IsNullOrEmpty(command.ParentTreeNodeId))
				section.SetParentTreeNodeId(new Guid(command.ParentTreeNodeId));
			section.SetName(command.Name);
			if (!string.IsNullOrEmpty(command.DefaultTreeNodeId))
				section.SetDefaultTreeNodeId(new Guid(command.DefaultTreeNodeId));
			section.SetSequence(command.Sequence);
			section.SetUrlSegment(command.UrlSegment);
		}
	}
}
