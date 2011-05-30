using System.Collections.Generic;

namespace Bennington.Cms.Models
{
    public class EmptySectionMenuItemRegistry : ISectionMenuItemRegistry
    {
        public IEnumerable<SectionMenuItem> GetItems()
        {
            return new SectionMenuItem[] {};
        }
    }
}