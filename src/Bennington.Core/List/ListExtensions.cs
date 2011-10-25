using System.Web.Helpers;

namespace Bennington.Core.List
{
    public static class ListExtensions
    {
        public static SortDirection Opposite(this SortDirection? sortDirection)
        {
            return sortDirection.GetValueOrDefault() == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
        }

        public static object GetValue(this ListColumn column, object listItem)
        {
            var propertyInfo = listItem.GetType().GetProperty(column.Name);
            return propertyInfo == null ? null : propertyInfo.GetValue(listItem, null);
        }
    }
}