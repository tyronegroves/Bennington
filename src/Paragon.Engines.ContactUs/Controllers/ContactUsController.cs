using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.Helpers;
using Paragon.ContentTree.Repositories;

namespace Paragon.Engines.ContactUs.Controllers
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
