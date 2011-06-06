using System.Collections.Generic;

namespace Bennington.Cms.Models
{
    public class ListPageViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}