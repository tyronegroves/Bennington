using System;
using System.Collections.Generic;
using Bennington.Cms.Buttons;
using Bennington.Cms.Metadata;
using Bennington.Cms.Models;
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
    }

    public class TestingIndividualLines : MetadataAttribute
    {
    }

    public class TestingIndividualLinesHandler : LoadTheseButtonsForEachRow<LocationViewModel>, IMetadataAttributeHandler<TestingIndividualLines>
    {
    }

    public class Testing : IListPageListPageButtonRegistry<LocationViewModel>
    {
        public IEnumerable<Button> GetTheTopRightButtons()
        {
            return new[] {new Button{Id = "CreateButton", Text="Create"}};
        }

        public IEnumerable<Button> GetTheBottomRightButtons()
        {
            return new[] { new Button { Id = "DeleteButton", Text = "Delete" } };
        }
    }
}