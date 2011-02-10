using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Models
{
	public class ToolLink : IAmATreeNodeExtension
	{
		public string Name { get; set; }
		public string UrlSegment { get; set; }
		public string TreeNodeId { get; set; }
		public int? Sequence { get; set; }
	}
}