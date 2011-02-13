using System.Web.Mvc;
using Bennington.ContentTree.TreeManager.Models;
using Bennington.ContentTree.TreeManager.ViewModelBuilders;
using Paragon.ContentTree;

namespace Bennington.ContentTree.TreeManager.Controllers
{
	public class TreeManagerController : Controller
	{
		private readonly ITreeBranchViewModelBuilder treeBranchViewModelBuilder;

		public TreeManagerController(ITreeBranchViewModelBuilder treeBranchViewModelBuilder)
		{
			this.treeBranchViewModelBuilder = treeBranchViewModelBuilder;
		}

		[Authorize]
		public ActionResult Index()
		{
			return View("Index", new Models.TreeManagerIndexViewModel()
			                     	{
			                     		TreeNodeCreationInputModel = new TreeNodeCreationInputModel()
			                     		                             	{
			                     		                             		ParentTreeNodeId = Constants.RootNodeId,
			                     		                             	}
			                     	});
		}

		[Authorize]
		public ActionResult Branch(string id)
		{
			return View("Branch", treeBranchViewModelBuilder.BuildViewModel(id));
		}
	}
}