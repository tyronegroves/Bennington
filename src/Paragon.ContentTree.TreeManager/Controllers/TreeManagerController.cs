using System.Web.Mvc;
using Paragon.ContentTree.TreeManager.ViewModelBuilders;

namespace Paragon.ContentTree.TreeManager.Controllers
{
	public class TreeManagerController : Controller
	{
		private readonly ITreeBranchViewModelBuilder treeBranchViewModelBuilder;

		public TreeManagerController(ITreeBranchViewModelBuilder treeBranchViewModelBuilder)
		{
			this.treeBranchViewModelBuilder = treeBranchViewModelBuilder;
		}

		public ActionResult Index()
		{
			return View("Index");
		}

		public ActionResult Branch(string id)
		{
			return View("Branch", treeBranchViewModelBuilder.BuildViewModel(id));
		}
	}
}