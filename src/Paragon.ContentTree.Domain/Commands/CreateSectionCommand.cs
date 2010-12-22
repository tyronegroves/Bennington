using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.Commands
{
	public class CreateSectionCommand : CommandWithAggregateRootId
	{
		public string ParentId { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string DefaultPageId { get; set; }
		public string Name { get; set; }
	}
}
