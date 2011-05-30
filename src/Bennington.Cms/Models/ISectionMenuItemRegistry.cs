using System.Collections.Generic;

namespace Bennington.Cms.Models
{
    public interface ISectionMenuItemRegistry
    {
        IEnumerable<SectionMenuItem> GetItems();
    }
}