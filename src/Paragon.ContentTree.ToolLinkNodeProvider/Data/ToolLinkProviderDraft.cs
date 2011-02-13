using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paragon.ContentTree.ToolLinkNodeProvider.Data
{
	public class ToolLinkProviderDraft
	{
		public string ToolLinkId { get; set; }
		public string TreeNodeId { get; set; }
		public int? Sequence { get; set; }
		public string Name { get; set; }
		public string UrlSegment { get; set; }
		public string Url { get; set; }
	}
}