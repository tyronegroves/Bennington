using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.Models;
using SampleApp.Models;

namespace SampleApp.Controllers
{
    public class LocationController : Controller
    {
        public ActionResult Edit(string id)
        {
            return View();
        }

        public ActionResult Index(int? page)
        {
            return View("Index", new ListPageViewModel<LocationViewModel>
                                     {
                                         Items = new[]
                                                     {
                                                         new LocationViewModel
                                                             {
                                                                 Id = "test1",
                                                                 City = "ZOlathe",
                                                                 Country = "USA",
                                                                 Description = "The description",
                                                                 State = "MO"
                                                             },
                                                         new LocationViewModel
                                                             {
                                                                 Id = "test2",
                                                                 City = "Kansas City",
                                                                 Country = "USA",
                                                                 Description = "Okie dokie",
                                                                 State = "KS"
                                                             }
                                                     }.AsQueryable()
                                     });
        }
    }
}