using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Models
{
	public class ContentTreeNodeInputModel
	{
		[HiddenInput(DisplayValue = false)]
		public string Action { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string FormAction { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string Type { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string TreeNodeId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string PageId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ParentTreeNodeId { get; set; }

		public string Name { get; set; }

		[DisplayName("Header Text")]
		public string HeaderText { get; set; }

		public int? Sequence { get; set; }

		[DisplayName("Url Segment")]
		public string UrlSegment { get; set; }

		[DataType(DataType.MultilineText)]
		public string Body { get; set; }

		[DisplayName("Hidden?")]
		public bool Hidden { get; set; }

		[DisplayName("Inactive?")]
		public bool Inactive { get; set; }
	}
}
