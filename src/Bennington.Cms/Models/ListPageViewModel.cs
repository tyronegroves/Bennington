using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bennington.Cms.Models
{
    public class ListPageViewModel
    {
        public IEnumerable<object> Items { get; set; }
    }
}