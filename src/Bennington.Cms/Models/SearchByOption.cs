using System;
using System.Collections.Generic;
using System.Linq;

namespace Bennington.Cms.Models
{
    public class SearchByOptions<T> : SearchByOptions
    {
        internal Func<IQueryable<T>> Items { get; set; }

        public virtual IQueryable<T> GetItems(string searchBy, string searchValue)
        {
            return Items();
        }
    }

    public class SearchByOptions
    {
        public SearchByOptions()
        {
            Options = new Dictionary<string, string>();
        }

        protected bool StartOptionsWithASelectLine { get; set; }

        public IDictionary<string, string> Options { get; set; }
    }
}