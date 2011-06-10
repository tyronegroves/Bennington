using System.Collections.Generic;
using Bennington.Cms.Buttons;
using Bennington.Cms.Metadata;
using Bennington.Cms.Models;

namespace SampleApp.Models
{
    [SetSectionHeaderOnListPageTo("TESTING 1234")]
    [SetGridHeaderOnListPageTo("Displaying stuff")]
    public class LocationViewModel
    {
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }

    public class Testing : IListPageListPageButtonRegistry<LocationViewModel>
    {
        public IEnumerable<Button> GetTheTopRightButtons()
        {
            return new[] {new Button{Id = "CreateButton", Text="Create"}};
        }
    }
}