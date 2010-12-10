using System.ComponentModel;
using System.Web.Mvc;

namespace Paragon.ContentTree.ExampleEngineNodeProvider.Models
{
	public class ContentTreeContactUsNodeInputModel
	{
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
	}
}
