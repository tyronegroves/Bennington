using Bennington.ContentTree.Domain.AggregateRoots;
using Bennington.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Bennington.ContentTree.Domain.CommandHandlers
{
	public class CreatePageCommandHandler : CommandHandler<CreatePageCommand>
	{
		private readonly IDomainRepository domainRepository;

		public CreatePageCommandHandler(IDomainRepository domainRepository)
		{
			this.domainRepository = domainRepository;
		}

		public override void Handle(CreatePageCommand command)
		{
			var page = new Page(command.PageId);
			page.SetTreeNodeId(command.TreeNodeId);
			page.SetActionId(command.Action ?? "Index");
			page.SetActive(command.Active);
			page.SetHidden(command.Hidden);
			page.SetType(command.Type);
			page.SetBody(command.Body);
			page.SetName(command.Name);
			page.SetHeaderText(command.HeaderText);
			page.SetUrlSegment(command.UrlSegment);
			page.SetMetaTitle(command.MetaTitle);
			page.SetMetaDescription(command.MetaDescription);
			page.SetMetaKeyword(command.MetaKeyword);
			page.SetSequence(command.Sequence);
			domainRepository.Save(page);
		}
	}
}
