namespace Bennington.Cms.Models
{
    public class SectionMenuItem
    {
        public string Name { get; set; }
    }

    public class SectionMenuItemForAControllerAction : SectionMenuItem
    {
        public string Controller { get; set; }

        public string Action { get; set; }
    }
}