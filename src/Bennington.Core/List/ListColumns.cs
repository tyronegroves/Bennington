using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bennington.Core.List
{
    public class ListColumns : Collection<ListColumn>
    {
        public ListColumn Find(string name)
        {
            return this.FirstOrDefault(listColumn => listColumn.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool Exists(string name)
        {
            return this.Any(listColumn => listColumn.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<ListColumn> Sorted()
        {
            return this.OrderBy(listColumn => listColumn.Order).ToList();
        }
    }
}