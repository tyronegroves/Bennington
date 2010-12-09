using System.Web.Mvc;
using Paragon.ContentTree.Models;
using Paragon.ContentTree.ViewModelBuilders;

namespace Paragon.ContentTree.Controllers
{
	public class ContentTreeController : Controller
	{
		private readonly ITreeBranchViewModelBuilder treeBranchViewModelBuilder;

		public ContentTreeController(ITreeBranchViewModelBuilder treeBranchViewModelBuilder)
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