using System.Web.Mvc;
using Bennington.Cms.Attributes;
using Bennington.ContentTree.TreeManager.Models;
using Bennington.ContentTree.TreeManager.ViewModelBuilders;

namespace Bennington.ContentTree.TreeManager.Controllers
{
	public class TreeManagerController : Controller
	{
		private readonly ITreeBranchViewModelBuilder treeBranchViewModelBuilder;
        private readonly ITreeManagerIndexViewModelBuilder treeManagerIndexViewModelBuilder;

        public TreeManagerController(ITreeBranchViewModelBuilder treeBranchViewModelBuilder,
                                    ITreeManagerIndexViewModelBuilder treeManagerIndexViewModelBuilder)
        {
            this.treeManagerIndexViewModelBuilder = treeManagerIndexViewModelBuilder;
            this.treeBranchViewModelBuilder = treeBranchViewModelBuilder;
        }

        [Authorize]
		public virtual ActionResult Index()
		{
			return View("Index", treeManagerIndexViewModelBuilder.BuildViewModel());
		}

		[Authorize]
        [Bennington.Cms.Attributes.DoNotUseTheDefaultMasterPage]
        public virtual ActionResult Branch(string id)
		{
			return View("Branch", "Blank", treeBranchViewModelBuilder.BuildViewModel(id));
		}
	}
}