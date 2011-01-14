﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Commanding;

namespace Paragon.ContentTree.Domain.Commands
{
	public class CreatePageCommand : CommandWithAggregateRootId
	{
		public Type Type { get; set; }
		public string ParentId { get; set; }
		public string HeaderText { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string Body { get; set; }
		//public string TreeNodeId { get; set; }

		public Guid PageId
		{
			get { return AggregateRootId; }
			set { AggregateRootId = value; }
		}
	}
}
