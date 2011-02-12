using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paragon.ContentTree.ContentNodeProvider.Data
{
	public class ContentNodeProviderDraft
	{
		public int Key { get; set; }
		public string PageId { get; set; }
		public string TreeNodeId { get; set; }
		public string UrlSegment { get; set; }
		public int? Sequence { get; set; }
		public string Name { get; set; }
		public string Action { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescription { get; set; }
		public string MetaKeyword { get; set; }
		public string HeaderText { get; set; }
		public string Body { get; set; }
	}
}
