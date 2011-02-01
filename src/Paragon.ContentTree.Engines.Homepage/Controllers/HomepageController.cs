using System.Web.Mvc;
using Paragon.ContentTree.ContentNodeProvider.Controllers;

namespace Paragon.ContentTree.Engines.Homepage.Controllers
{
	[HandleError]
	public class HomepageController : EngineController
	{
		public ActionResult Index()
		{
			return View();
		}

		public override string Name
		{
			get { return "Homepage"; }
		}
	}
}
