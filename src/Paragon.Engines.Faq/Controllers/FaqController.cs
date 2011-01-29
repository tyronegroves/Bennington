using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paragon.ContentTree.ContentNodeProvider.Controllers;

namespace Paragon.Engines.Faq.Controllers
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
