using System.Web.Mvc;
using Bennington.ContentTree.Providers.ContentNodeProvider.Controllers;

namespace Bennington.ContentTree.Engines.Faq.Controllers
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
