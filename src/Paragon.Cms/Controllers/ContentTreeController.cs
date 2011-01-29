using System.Web.Mvc;
using Paragon.Cms.ViewModelBuilders;

namespace Paragon.Cms.Controllers
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