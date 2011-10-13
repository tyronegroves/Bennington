using System.ComponentModel;
using System.Web.Mvc;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Models
{
	public class ToolLinkInputModel
	{
        [DisplayName("Inactive?")]
        public bool Inactive { get; set; }

        [DisplayName("Hidden?")]
        public bool Hidden { get; set; }

        [HiddenInput(DisplayValue = false)]
		public string SectionId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string Action { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string FormAction { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string TreeNodeId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ParentTreeNodeId { get; set; }

        public string Name { get; set; }

        [DisplayName("Url Segment")]
        public string UrlSegment { get; set; }

		public int? Sequence { get; set; }

		public string Url { get; set; }
	}
}