using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.Commands
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
