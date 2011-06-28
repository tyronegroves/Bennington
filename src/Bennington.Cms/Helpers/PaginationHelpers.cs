using PagedList;

namespace Bennington.Cms.Helpers
{
    public static class PaginationHelpers
    {
        public static PaginationRange GetPageRange(this IPagedList pagedList, int i)
        {
            if (pagedList.TotalItemCount <= 0) return null;

            if (pagedList.TotalItemCount < i * pagedList.PageSize) return null;

            var end = (pagedList.PageSize * (i + 1));

            if (pagedList.TotalItemCount < end)
                end = pagedList.TotalItemCount;

            var begin = 1 + i*pagedList.PageSize;
            if (i == 0) begin = 1;

            return new PaginationRange {Begin = begin, End = end};
        }
    }

    public class PaginationRange
    {
        public int Begin { get; set; }

        public int End { get; set; }

        public override string ToString()
        {
            if (Begin == End) return Begin.ToString();
            return string.Format("{0} - {1}", Begin, End);
        }
    }
}