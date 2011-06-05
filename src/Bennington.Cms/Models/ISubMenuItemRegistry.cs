using System.Collections.Generic;

namespace Bennington.Cms.Models
{
    public interface ISubMenuItemRegistry
    {
        IEnumerable<SubMenuItem> GetTheSubMenuItems();
    }
}