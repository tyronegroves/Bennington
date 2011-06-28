using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Bennington.Cms.Metadata;
using Bennington.Cms.Sorting;
using MvcTurbine.ComponentModel;
using PagedList;

namespace Bennington.Cms.Models
{
    [LoadListPageButtons]
    public class ListPageViewModel<T>
    {
        private readonly IPaginationStateRetriever paginationStateRetriever;
        private ISearchStateRetriever searchStateRetriever;

        public ListPageViewModel()
        {
            try
            {
                paginationStateRetriever = ServiceLocatorManager.Current.Resolve<IPaginationStateRetriever>();
                searchStateRetriever = ServiceLocatorManager.Current.Resolve<ISearchStateRetriever>();
            } catch
            {
            }
        }

        public SearchByOptions<T> SearchByOptions { get; private set; }
        public void SetSearchByOptions(SearchByOptions<T> searchByOptions)
        {
            searchByOptions.Items = () => Items;
            SearchByOptions = searchByOptions;
        }

        public IQueryable<T> Items { get; set; }

        public virtual PaginationState PaginationState
        {
            get { return paginationStateRetriever.GetTheCurrentPaginationState(typeof (T)); }
        }

        public virtual IPagedList<T> PagedItems
        {
            get
            {
                var searchState = searchStateRetriever.GetTheCurrnetSearchState(typeof (T));
                var items = Items;
                if (searchState.IsSearching && this.SearchByOptions != null)
                    items = SearchByOptions.GetItems(searchState.SearchBy, searchState.SearchValue);
                
                var paginationState = paginationStateRetriever.GetTheCurrentPaginationState(typeof (T));

                try
                {
                    var sortBy = paginationState.SortBy;

                    var expression = Pagination.CreateLambdaExpression(sortBy, typeof (T));

                    if (paginationState.SortOrder == "desc")
                        return Pagination.ApplyOrderByDescending(items, expression)
                            .ToPagedList(paginationState.CurrentPage, paginationState.PageSize);

                    return Pagination.ApplyOrderBy(items, expression)
                        .ToPagedList(paginationState.CurrentPage, paginationState.PageSize);
                }
                catch
                {
                    return items.ToPagedList(paginationState.CurrentPage, paginationState.PageSize);
                }
            }
        }
    }

    public static class Pagination
    {
        private static readonly MethodInfo orderByMethod;
        private static readonly MethodInfo orderByDescendingMethod;
        //private static MethodInfo thenByMethod;
        //private static MethodInfo thenByDescendingMethod;

        static Pagination()
        {
            orderByMethod = FindEnumerableMethod("OrderBy");
            orderByDescendingMethod = FindEnumerableMethod("OrderByDescending");
            //thenByMethod = FindEnumerableMethod("ThenBy");
            //thenByDescendingMethod = FindEnumerableMethod("ThenByDescending");
        }

        public static IEnumerable<T> ApplyOrderBy<T>(IEnumerable<T> entities, LambdaExpression expression)
        {
            return (IEnumerable<T>) orderByMethod.MakeGenericMethod(typeof (T), expression.Type.GetGenericArguments()[1])
                                        .Invoke(null, new object[] {entities, expression.Compile()});
        }

        public static IEnumerable<T> ApplyOrderByDescending<T>(IEnumerable<T> entities, LambdaExpression expression)
        {
            return (IEnumerable<T>) orderByDescendingMethod.MakeGenericMethod(typeof (T), expression.Type.GetGenericArguments()[1])
                                        .Invoke(null, new object[] {entities, expression.Compile()});
        }

        private static MethodInfo FindEnumerableMethod(string name)
        {
            return (MethodInfo) typeof (Enumerable).FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Static,
                                                                delegate(MemberInfo m, object o)
                                                                    {
                                                                        if (m.Name != name)
                                                                            return false;

                                                                        var parameters = ((MethodInfo) m).GetParameters();

                                                                        return parameters.Length == 2 && parameters[0].Name == "source" &&
                                                                               parameters[1].Name == "keySelector";
                                                                    }, null).Single();
        }

        public static LambdaExpression CreateLambdaExpression(string propertyName, Type entityType)
        {
            var parameterExpression = Expression.Parameter(entityType, "it");
            var memberExpression = Expression.PropertyOrField(parameterExpression, propertyName);

            // We need to create a type for the delegate which will be always take
            // a single argument and return a single value so its a Func<T,TResult> type
            var delegateType = typeof (Func<,>).MakeGenericType(entityType, memberExpression.Type);

            return Expression.Lambda(delegateType, memberExpression, parameterExpression);
        }
    }
}