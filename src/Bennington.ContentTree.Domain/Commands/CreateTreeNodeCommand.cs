using System;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.Commands
{
	public class CreateTreeNodeCommand : CommandWithAggregateRootId
	{
		public Type Type { get; set; }
		public string ParentId { get; set; }

		public Guid TreeNodeId
		{
			get { return AggregateRootId; }
			set { AggregateRootId = value; }
		}
	}
}
