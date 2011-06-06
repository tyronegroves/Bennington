using System.Web.Mvc;
using Bennington.Cms.Models;
using SampleApp.Models;

namespace SampleApp.Controllers
{
    public class LocationController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", new ListPageViewModel<LocationViewModel>
                                     {
                                         Items = new []{
                                         new LocationViewModel
                                             {
                                                 City = "Olathe",
                                                 Country = "USA",
                                                 Description = "The description",
                                                 State = "MO"
                                             }}
                                     });
        }
    }
}