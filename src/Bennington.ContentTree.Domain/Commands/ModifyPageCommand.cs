using System;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Domain.Commands
{
	public class ModifyPageCommand : CommandWithAggregateRootId
	{
		public string Name {get; set;}
		public bool Hidden { get; set; }
		public bool Inactive { get; set; }
		public string ActionId { get; set; }
		public string ParentId { get; set; }
		public string HeaderText { get; set; }
		public string HeaderImage { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string Body { get; set; }
        public string LastModifyBy { get; set; }

		public Guid PageId
		{
			get { return AggregateRootId; }
			set { AggregateRootId = value; }
		}
		public Guid TreeNodeId { get; set; }
	}
}
