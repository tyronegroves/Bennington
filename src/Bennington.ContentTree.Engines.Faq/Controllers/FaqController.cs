using System.Web.Mvc;
using Paragon.ContentTree.ContentNodeProvider.Controllers;

namespace Paragon.ContentTree.Engines.Faq.Controllers
{
	[HandleError]
	public class FaqController : EngineController
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public override string Name
		{
			get { return "Faq Engine"; }
		}
	}
}
