namespace Bennington.Cms.Models
{
    public interface ISectionMenuRetriever
    {
        SectionMenu GetTheSectionMenu();
    }

    public class SectionMenuRetriever : ISectionMenuRetriever
    {
        private readonly ISectionMenuItemRegistry sectionMenuItemRegistry;

        public SectionMenuRetriever(ISectionMenuItemRegistry sectionMenuItemRegistry)
        {
            this.sectionMenuItemRegistry = sectionMenuItemRegistry;
        }

        public SectionMenu GetTheSectionMenu()
        {
            return new SectionMenu
                       {
                           Items = sectionMenuItemRegistry.GetItems()
                       };
        }
    }
}