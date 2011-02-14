using Bennington.ContentTree.Domain.AggregateRoots;
using Bennington.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.CommandHandlers
{
	public class ModifyPageCommandHandler : AggregateRootCommandHandler<ModifyPageCommand, Page>
	{
		public override void Handle(ModifyPageCommand command, Page page)
		{
			page.PageId = command.AggregateRootId;
			page.SetBody(command.Body);
			page.SetName(command.Name);
			page.SetHeaderText(command.HeaderText);
			page.SetSequence(command.Sequence);
			page.SetUrlSegment(command.UrlSegment);
			page.SetActionId(command.ActionId);
		}
	}
}
