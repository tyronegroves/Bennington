using System;
using System.Web.Mvc;
using Paragon.ContentTree.ContentNodeProvider.Context;
using Paragon.ContentTree.ContentNodeProvider.Controllers;
using Paragon.ContentTree.Controllers;
using Paragon.ContentTree.Helpers;
using Paragon.ContentTree.Repositories;

namespace Webroot.Controllers
{
    [HandleError]
	public class HomeController : Controller
    {

    	public ActionResult Index()
        {
            return View();
        }
    }
}