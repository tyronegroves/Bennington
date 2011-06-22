﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bennington.Cms.Buttons;
using Bennington.Cms.Metadata;
using Bennington.Cms.Models;
using FluentValidation;
using MvcTurbine.Web.Metadata;
using SampleApp.Models;

namespace SampleApp.Controllers
{
    public class LocationForm : EditForm
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? FoundedOn { get; set; }

        [ConsoleOptions]
        public List<string> Options { get; set; }
    }

    public class ConsoleOptions : ConsoleAttribute { }

    public class ConsoleOptionsAttributeHandler : ConsoleAttributeHandler<ConsoleOptions>
    {
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
        }
    }

    public class LocationFormButtons : IEditPageButtonRegistry<LocationForm>
    {
        public IEnumerable<Button> GetTheActionButtons()
        {
            return new[] { new SubmitButton { Id = "Save", Text = "Save" } };
        }
    }

    public class LocationController : Controller
    {
        public ActionResult Edit(string id)
        {
            return View("Edit", new LocationForm { FirstName = "Darren", FoundedOn = null, Options = new[] { "one", "two" }.ToList() });
        }

        [HttpPost]
        public ActionResult Edit(LocationForm locationForm)
        {
            return View("Edit", locationForm);
        }

        public ActionResult Index()
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