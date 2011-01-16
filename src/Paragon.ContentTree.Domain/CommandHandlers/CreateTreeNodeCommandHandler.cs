using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Domain.AggregateRoots;
using Paragon.ContentTree.Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Paragon.ContentTree.Domain.CommandHandlers
{
	public class CreateTreeNodeCommandHandler : CommandHandler<CreateTreeNodeCommand>
	{
		private readonly IDomainRepository domainRepository;

		public CreateTreeNodeCommandHandler(IDomainRepository domainRepository)
		{
			this.domainRepository = domainRepository;
		}

		public override void Handle(CreateTreeNodeCommand command)
		{
			var treeNode = new TreeNode(command.AggregateRootId);
			treeNode.SetParentTreeNodeId(new Guid(command.ParentId));
			treeNode.SetType(command.Type);

			domainRepository.Save(treeNode);
		}
	}
}
