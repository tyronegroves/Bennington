namespace Bennington.Cms.Models
{
    public class SubMenuRetriever : ISubMenuRetriever
    {
        public SubMenu GetTheSubMenu()
        {
            return new SubMenu {Items = new SubMenuItem[] {}};
        }
    }
}