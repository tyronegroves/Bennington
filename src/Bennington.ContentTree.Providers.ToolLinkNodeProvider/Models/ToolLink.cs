using System;
using Bennington.ContentTree.Models;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models
{
	public class ToolLink : IAmATreeNodeExtension
	{
		public string Name { get; set; }
		public string UrlSegment { get; set; }
		public string TreeNodeId { get; set; }
		public int? Sequence { get; set; }
		public bool Hidden { get; set; }
		public bool Inactive { get; set; }
        public string IconUrl { get; set; }
	    public DateTime LastModifyDate { get; set; }
	    public string LastModifyBy { get; set; }
	}
}