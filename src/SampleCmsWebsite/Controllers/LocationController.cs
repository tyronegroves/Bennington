using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.Buttons;
using Bennington.Cms.Metadata;
using Bennington.Cms.Models;
using FluentValidation;
using SampleCmsWebsite.Models;

namespace SampleCmsWebsite.Controllers
{


    public class LocationController : Controller
    {
        public ActionResult Edit(string id)
        {
            return View("Edit", new LocationForm { FirstName = "Darren", FoundedOn = null, Options = new[] { "one", "two" }.ToList() });
        }

        [HttpPost]
        public ActionResult Edit(LocationForm locationForm)
        {
            throw new NotImplementedException();
            //Flash.Notify(string.Format("Location {0} saved.", locationForm.FirstName));
            //return View("Edit", locationForm);
        }

        public ActionResult Index()
        {
            var listPageViewModel = new ListPageViewModel<LocationViewModel>
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
                                                                            Id = "test1",
                                                                            City = "ZOlathe",
                                                                            Country = "USA",
                                                                            Description = "The description",
                                                                            State = "MO"
                                                                        },
                                                            #region initialize Items property
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                                                Id = "test1",
                                                                                City = "ZOlathe",
                                                                                Country = "USA",
                                                                                Description = "The description",
                                                                                State = "MO"
                                                                            },
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                                Id = "test1",
                                                                City = "ZOlathe",
                                                                Country = "USA",
                                                                Description = "The description",
                                                                State = "MO"
                                                                },
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
                                                            #endregion
                                                            }.AsQueryable()
                                            };

            listPageViewModel.SetSearchByOptions(new LocationSearchByOptions());
            return View("Index", listPageViewModel);
        }
    }

    public class LocationSearchByOptions : SearchByOptions<LocationViewModel>
    {
        public LocationSearchByOptions()
        {
            this.Options.Add("Name", "Name");
            this.Options.Add("Other", "Other");
        }

        public override IQueryable<LocationViewModel> GetItems(string searchBy, string searchValue)
        {
            var locationViewModels = base.GetItems(searchBy, searchValue);
            switch (searchBy)
            {
                case "Name":
                    return locationViewModels.Where(x => x.State.Contains(searchValue));
            }
            return locationViewModels;
        }
    }

    public class LocationForm : EditForm
    {
        public string FirstName { get; set; }
        [HtmlEditor]
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? FoundedOn { get; set; }

        [ConsoleOptions]
        public List<string> Options { get; set; }

        [ConsoleOptions]
        public List<string> MoreOptions { get; set; }
    }

    public class ConsoleOptions : ConsoleAttribute { }

    public class ConsoleOptionsAttributeHandler : ConsoleAttributeHandler<ConsoleOptions>
    {
        public ConsoleOptionsAttributeHandler()
        {
            LeftLabel = "Applesauce";
            RightLabel = "Carrots";
        }

        public override IEnumerable<SelectListItem> GetItems()
        {
            return new[] { "one", "two", "three", "four" }
                .Select(x => new SelectListItem { Text = x, Value = x });

        }
    }
    
    public class LocationFormValidator : AbstractValidator<LocationForm>
    {
        public LocationFormValidator()
        {
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("This field is required.");

            RuleFor(x => x.Options)
                .Must(x => false)
                .When(x => x.Options == null || x.Options.Count() == 0)
                .WithMessage("UH OH!");
        }
    }

    public class LocationFormButtons : IEditPageButtonRegistry<LocationForm>
    {
        public IEnumerable<Button> GetTheActionButtons(LocationForm adminAccountEditForm)
        {
            return new[] { new SubmitButton { Id = "Save", Text = "Save" } };
        }
    }
}