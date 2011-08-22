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

        public ActionResult About()
        {
            return View();
        }

        public override string Name
        {
            get { return "Example Controller"; }
        }
    }
}
