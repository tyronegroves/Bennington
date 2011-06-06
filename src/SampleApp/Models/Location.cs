using Bennington.Cms.Metadata;

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
}