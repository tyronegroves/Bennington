using System.Collections.Generic;
using Paragon.ContentTree.Models;

namespace Paragon.ContentTree.ContentNodeProvider.Models
{
	public class ContentItemNavigationViewModel
	{
		public string TreeNodeId { get; set; }
		public IEnumerable<ContentTreeNodeContentItem> ContentTreeNodeContentItems { get; set; }
	}
}
