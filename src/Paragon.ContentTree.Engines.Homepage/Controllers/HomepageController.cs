using System;
using System.Web.Mvc;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.Engines.Homepage.ViewModelBuilder;

namespace Paragon.ContentTree.Engines.Homepage.Controllers
{
	[HandleError]
	public class HomepageController : EngineController
	{
		private readonly IHomepageIndexViewModelBuilder homepageIndexViewModelBuilder;

		public HomepageController(IHomepageIndexViewModelBuilder homepageIndexViewModelBuilder)
		{
			this.homepageIndexViewModelBuilder = homepageIndexViewModelBuilder;
		}

		public ActionResult Index()
		{
			return View("Index", homepageIndexViewModelBuilder.BuildViewModel());
		}

		public override string Name
		{
			get { return "Homepage"; }
		}

		//public override string ActionToUseForModification
		//{
		//    get
		//    {
		//        return string.Empty;
		//    }
		//    set
		//    {
		//        throw new NotImplementedException();
		//    }
		//}
	}
}
