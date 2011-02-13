using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bennington.ContentTree.TreeManager.Models
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