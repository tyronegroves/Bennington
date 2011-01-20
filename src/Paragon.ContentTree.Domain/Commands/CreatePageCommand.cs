using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.Commands
{
	public class CreatePageCommand : CommandWithAggregateRootId
	{
		public string Name { get; set; }
		public string MetaKeyword {get;set;}
		public string MetaDescription {get; set; }
		public Type Type { get; set; }
		public string HeaderText { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string Body { get; set; }
		public string MetaTitle { get; set; }
		public Guid TreeNodeId { get; set; }
		public string Action { get; set; }

		public Guid PageId
		{
			get { return AggregateRootId; }
			set { AggregateRootId = value; }
		}
	}
}
