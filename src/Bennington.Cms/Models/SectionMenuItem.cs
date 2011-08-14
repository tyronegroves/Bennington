namespace Bennington.Cms.Models
{
    public class SectionMenuItem
    {
        public string Name { get; set; }
        public bool Selected { get; set; }
    }

    public class SectionMenuItemForAControllerAction : SectionMenuItem
    {
        public string Controller { get; set; }

        public string Action { get; set; }
    }

    public class SectionMenuItemForAUrl : SectionMenuItem
    {
        public string Url { get; set; }
    }
}