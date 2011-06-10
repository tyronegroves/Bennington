using System.Collections.Generic;
using Bennington.Cms.Metadata;

namespace Bennington.Cms.Models
{
    [LoadButtonsFromRegistry]
    public class ListPageViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}