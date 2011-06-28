using System;
using System.Web;

namespace Bennington.Cms.Sorting
{
    public interface ISearchStateRetriever
    {
        SearchState GetTheCurrnetSearchState(Type type);
    }

    public class SearchStateRetriever : ISearchStateRetriever
    {
        public SearchState GetTheCurrnetSearchState(Type type)
        {
            var searchBy = HttpContext.Current.Request.QueryString["searchBy"];
            var searchValue = HttpContext.Current.Request.QueryString["searchValue"];
            return new SearchState
                {
                    IsSearching = string.IsNullOrEmpty(searchBy) == false && string.IsNullOrEmpty(searchValue) == false,
                    SearchBy = searchBy,
                    SearchValue = searchValue
                };
        }
    }

    public class SearchState
    {
        public bool IsSearching { get; set; }

        public string SearchBy { get; set; }

        public string SearchValue { get; set; }
    }

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
                           PageSize = 10,
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