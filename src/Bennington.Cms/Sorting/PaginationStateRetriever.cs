using System;
using System.Web;

namespace Bennington.Cms.Sorting
{
    public interface IPaginationStateRetriever
    {
        PaginationState GetTheCurrentPaginationState(Type type);
    }

    public class PaginationStateRetriever : IPaginationStateRetriever
    {
        public PaginationState GetTheCurrentPaginationState(Type type)
        {
            return new PaginationState
                       {
                           SortBy = GetSortBy(),
                           SortOrder = GetSortOrder(),
                           PageSize = 20,
                           CurrentPage = GetThePage(),
                       };
        }

        private static string GetSortOrder()
        {
            return HttpContext.Current.Request.QueryString["sortOrder"] == "desc" ? "desc" : "asc";
        }

        private static string GetSortBy()
        {
            return HttpContext.Current.Request.QueryString["sortBy"];
        }

        private static int GetThePage()
        {
            int page;
            int.TryParse(HttpContext.Current.Request.QueryString["page"], out page);
            return page;
        }
    }

    public class PaginationState
    {
        public string SortBy { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public string SortOrder { get; set; }
    }
}