using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Domain.AggregateRoots;
using Paragon.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.CommandHandlers
{
	public class CreatePageCommandHandler : AggregateRootCommandHandler<CreatePageCommand, Page>
	{
		public override void Handle(CreatePageCommand command, Page page)
		{
			page.SetParentId(command.ParentId);
			page.SetBody(command.Body);
			page.SetHeaderText(command.HeaderText);
			page.SetSequence(command.Sequence);
			page.SetUrlSegment(command.UrlSegment);
		}
	}
}
