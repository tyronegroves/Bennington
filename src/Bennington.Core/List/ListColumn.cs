using System;
using System.Web.Helpers;

namespace Bennington.Core.List
{
    public class ListColumn
    {
        public bool IsSearchable { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public bool IsSorted { get; set; }
        public SortDirection? SortDirection { get; set; }
        public string SortUrl { get; set; }
        public int Order { get; set; }
        public bool HasCellTemplate { get; set; }
        public string CellTemplate { get; set; }
        public bool HasHeaderTemplate { get; set; }
        public string HeaderTemplate { get; set; }
        public Type Type { get; set; }
    }
}