using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.Buttons;
using Bennington.Cms.Models;
using SampleApp.Models;

namespace SampleApp.Controllers
{
    public class LocationForm : EditForm
    {
    }

    public class LocationFormButtons : IEditPageButtonRegistry<LocationForm>
    {
        public IEnumerable<Button> GetTheActionButtons()
        {
            return new[] {new Button {Id = "Save", Text = "Save"}};
        }
    }

    public class LocationController : Controller
    {
        public ActionResult Edit(string id)
        {
            return View("Edit", new LocationForm());
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