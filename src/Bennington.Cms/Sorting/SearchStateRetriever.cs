using System;
using System.Web;

namespace Bennington.Cms.Sorting
{
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
}