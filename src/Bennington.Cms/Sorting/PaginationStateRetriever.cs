using System;
using System.Web;

namespace Bennington.Cms.Sorting
{
    public class PaginationStateRetriever : IPaginationStateRetriever
    {
        public PaginationState GetTheCurrentPaginationState(Type type)
        {
            return new PaginationState
                       {
                           SortBy = GetSortBy(),
                           SortOrder = GetSortOrder(),
                           PageSize = IsViewingAll() ? 1000 : 10,
                           CurrentPage = GetThePage(),
                           ViewingAll = IsViewingAll(),
                       };
        }

        private static bool IsViewingAll()
        {
            return PullThePageFromTheQuerystring() < 0;
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
            var page = PullThePageFromTheQuerystring();
            return page < 0 ? 0 : page;
        }

        private static int PullThePageFromTheQuerystring()
        {
            int page;
            int.TryParse(HttpContext.Current.Request.QueryString["page"], out page);
            return page;
        }
    }
}