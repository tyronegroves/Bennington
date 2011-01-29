using System.Web.Mvc;
using Paragon.ContentTree.ContentNodeProvider.Controllers;

namespace Paragon.ContentTree.Engines.ContactUs.Controllers
{
	[HandleError]
	public class ContactUsController : EngineController
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Confirmation()
		{
			return View();
		}

		public override string Name
		{
			get { return "Contact Us Engine"; }
		}
	}
}
