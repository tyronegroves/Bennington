using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bennington.Cms.Models
{
    public class ListPageViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}