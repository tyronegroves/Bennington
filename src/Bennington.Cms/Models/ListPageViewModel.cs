using System.Linq;
using Bennington.Cms.Metadata;

namespace Bennington.Cms.Models
{
    [LoadButtonsFromRegistry]
    public class ListPageViewModel<T>
    {
        public IQueryable<T> Items { get; set; }
    }
}