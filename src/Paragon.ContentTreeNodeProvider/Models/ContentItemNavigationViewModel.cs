using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTreeNodeProvider.Models
{
	public class ContentItemNavigationViewModel
	{
		public string TreeNodeId { get; set; }
		public IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems { get; set; }
	}
}
