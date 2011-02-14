using System;
using System.Web.Mvc;
using Bennington.ContentTree.Engines.Homepage.ViewModelBuilder;
using Bennington.ContentTree.Providers.ContentNodeProvider.Controllers;

namespace Bennington.ContentTree.Engines.Homepage.Controllers
{
	[HandleError]
	public class HomepageController : EngineController
	{
		private readonly IHomepageIndexViewModelBuilder homepageIndexViewModelBuilder;

		public override string Name
		{
			get { return "Homepage"; }
		}

		public override string ControllerToUseForModification
		{
			get
			{
				return "HomepageContentTreeNode";
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override string ControllerToUseForCreation
		{
			get
			{
				return "HomepageContentTreeNode";
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public HomepageController(IHomepageIndexViewModelBuilder homepageIndexViewModelBuilder)
		{
			this.homepageIndexViewModelBuilder = homepageIndexViewModelBuilder;
		}

		public ActionResult Index()
		{
			return View("Index", homepageIndexViewModelBuilder.BuildViewModel());
		}
	}
}
