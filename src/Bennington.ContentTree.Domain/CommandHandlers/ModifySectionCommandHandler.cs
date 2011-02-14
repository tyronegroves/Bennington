using System;
using Bennington.ContentTree.Domain.AggregateRoots;
using Bennington.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.CommandHandlers
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
