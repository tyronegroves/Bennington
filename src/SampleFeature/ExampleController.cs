using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Bennington.ContentTree.Providers.ContentNodeProvider.Controllers;

namespace SampleFeature
{
    public class ExampleController : EngineController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(string id, int? somethingElse)
        {
            return View("About", new AboutViewModel() { Id = id, SomethingElse = somethingElse });
        }

        public override string Name
        {
            get { return "Example Controller"; }
        }
    }

    public class AboutViewModel
    {
        public string Id { get; set; }

        public int? SomethingElse { get; set; }
    }
}
