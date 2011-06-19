using System;
using System.Collections.Generic;
using System.Web.Routing;
using Bennington.Cms.Buttons;
using Bennington.Cms.Metadata;
using MvcTurbine.Web.Metadata;

namespace SampleApp.Models
{
    [SetSectionHeaderOnListPageTo("TESTING 1234")]
    [SetGridHeaderOnListPageTo("Displaying stuff")]
    [TestingIndividualLines]
    public class LocationViewModel
    {
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public DateTime? FoundedOn { get; set;  }
    }

    public class TestingIndividualLines : MetadataAttribute
    {
    }

    public class TestingIndividualLinesHandler : LoadTheseButtonsForEachRow<LocationViewModel>, IMetadataAttributeHandler<TestingIndividualLines>
    {
        public override IEnumerable<Button> GetButtons(LocationViewModel model)
        {
            if (model == null) return new Button[] {};
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary["action"] = "Edit";
            routeValueDictionary["controller"] = "Location";
            routeValueDictionary["id"] = model.Id;

            return new[]
                       {
                           new RoutesButton {Id = "Edit", Text = "Edit", RouteValues = routeValueDictionary}
                       };
        }
    }

    public class Testing : IListPageButtonRegistry<LocationViewModel>
    {
        public IEnumerable<Button> GetTheTopRightButtons()
        {
            return new[] {new Button {Id = "CreateButton", Text = "Create"}};
        }

        public IEnumerable<Button> GetTheBottomRightButtons()
        {
            return new[] {new Button {Id = "DeleteButton", Text = "Delete"}};
        }
    }
}