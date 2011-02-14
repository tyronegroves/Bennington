using System;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.Commands
{
	public class DeletePageCommand : CommandWithAggregateRootId
	{
		public Guid TreeNodeId {get; set;}
	}
}
