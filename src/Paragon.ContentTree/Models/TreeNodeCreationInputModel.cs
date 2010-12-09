using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paragon.ContentTree.Models
{
	public class TreeNodeCreationInputModel
	{
		[HiddenInput(DisplayValue = false)]
		public string ParentTreeNodeId { get; set; }
		
		[DisplayName("Type")]
		[UIHint("ProviderTypes")]
		public string ProviderType { get; set; }
	}
}