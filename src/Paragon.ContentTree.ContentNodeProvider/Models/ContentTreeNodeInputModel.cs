using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Paragon.ContentTree.ContentNodeProvider.Models
{
	public class ContentTreeNodeInputModel
	{
		[HiddenInput(DisplayValue = false)]
		public string ContentItemId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string Action { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string Type { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string TreeNodeId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ParentTreeNodeId { get; set; }

		public string Name { get; set; }

		[DisplayName("Header Text")]
		public string HeaderText { get; set; }

		public int? Sequence { get; set; }

		[DisplayName("Url Segment")]
		public string UrlSegment { get; set; }

		[DataType(DataType.MultilineText)]
		public string Content { get; set; }
	}
}
