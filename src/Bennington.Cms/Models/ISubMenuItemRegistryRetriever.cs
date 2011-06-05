using System.Collections.Generic;

namespace Bennington.Cms.Models
{
    public interface ISubMenuItemRegistryRetriever
    {
        IEnumerable<ISubMenuItemRegistry> GetTheRegistries();
    }
}