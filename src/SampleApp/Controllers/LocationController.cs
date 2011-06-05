using System.Web.Mvc;
using SampleApp.Models;

namespace SampleApp.Controllers
{
    public class LocationController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", new[]
                                     {
                                         new LocationViewModel
                                             {
                                                 City = "Olathe",
                                                 Country = "USA",
                                                 Description = "The description",
                                                 State = "MO"
                                             }
                                     });
        }
    }
}