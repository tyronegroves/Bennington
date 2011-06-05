using System.Collections.Generic;

namespace Bennington.Cms.Models
{
    public class SubMenuRetriever : ISubMenuRetriever
    {
        private readonly ISubMenuItemRegistryRetriever subMenuItemRegistryRetriever;

        public SubMenuRetriever(ISubMenuItemRegistryRetriever subMenuItemRegistryRetriever)
        {
            this.subMenuItemRegistryRetriever = subMenuItemRegistryRetriever;
        }

        public SubMenu GetTheSubMenu()
        {
            return new SubMenu {Items = GetAllSubMenuItems()};
        }

        private IEnumerable<SubMenuItem> GetAllSubMenuItems()
        {
            var list = new List<SubMenuItem>();

            var registries = subMenuItemRegistryRetriever.GetTheRegistries();
            foreach (var registry in registries)
                list.AddRange(registry.GetTheSubMenuItems());
            return list;
        }
    }
}