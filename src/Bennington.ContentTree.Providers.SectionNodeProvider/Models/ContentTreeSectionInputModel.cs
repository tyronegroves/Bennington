using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Models
{
	public class ContentTreeSectionInputModel
	{
		[HiddenInput(DisplayValue = false)]
		public string SectionId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string Action { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string TreeNodeId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ParentTreeNodeId { get; set; }

		public string Name { get; set; }

		public int? Sequence { get; set; }

		[DisplayName("Url Segment")]
		public string UrlSegment { get; set; }

		[UIHint("DefaultTreeNodeId")]
		[DisplayName("Default Page")]
		public string DefaultTreeNodeId { get; set; }
	}
}
