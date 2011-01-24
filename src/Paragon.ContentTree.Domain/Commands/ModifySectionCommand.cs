using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.Commands
{
	public class ModifySectionCommand : CommandWithAggregateRootId
	{
		public string TreeNodeId { get; set; }
		public string ParentTreeNodeId { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string DefaultTreeNodeId { get; set; }
		public string Name { get; set; }
	}
}
