using System.ComponentModel;
using System.Web.Mvc;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models
{
	public class ToolLinkInputModel
	{
		[HiddenInput(DisplayValue = false)]
		public string SectionId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string Action { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string TreeNodeId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ParentTreeNodeId { get; set; }

		public int? Sequence { get; set; }

		[DisplayName("Url Segment")]
		public string UrlSegment { get; set; }

		public string Name { get; set; }

		public string Url { get; set; }
	}
}