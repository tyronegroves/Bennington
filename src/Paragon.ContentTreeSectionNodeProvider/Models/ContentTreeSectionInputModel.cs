using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Paragon.ContentTreeSectionNodeProvider.Models
{
	public class ContentTreeSectionInputModel
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

		[UIHint("DefaultTreeNodeId")]
		[DisplayName("Default Page")]
		public string DefaultTreeNodeId { get; set; }
	}
}
