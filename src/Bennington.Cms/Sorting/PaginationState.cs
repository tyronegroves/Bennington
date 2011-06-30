using System;

namespace Bennington.Cms.Sorting
{
    public class PaginationState
    {
        public string SortBy { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public string SortOrder { get; set; }

        public bool ViewingAll { get; set; }
    }
}