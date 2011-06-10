using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Bennington.Cms.Metadata;
using PagedList;

namespace Bennington.Cms.Models
{
    [LoadButtonsFromRegistry]
    public class ListPageViewModel<T>
    {
        public IQueryable<T> Items { get; set; }

        public IPagedList<T> PagedItems
        {
            get
            {
                var expression = Pagination.CreateLambdaExpression("Name", typeof(T));

                Pagination.ApplyOrderBy(Items, expression);

                return Items.ToPagedList(0, 5);
            }
        }

    }

    public static class Pagination
    {

        private static MethodInfo orderByMethod;
        //private static MethodInfo orderByDescendingMethod;
        //private static MethodInfo thenByMethod;
        //private static MethodInfo thenByDescendingMethod;

        static Pagination()
        {
            orderByMethod = FindEnumerableMethod("OrderBy");
            //orderByDescendingMethod = FindEnumerableMethod("OrderByDescending");
            //thenByMethod = FindEnumerableMethod("ThenBy");
            //thenByDescendingMethod = FindEnumerableMethod("ThenByDescending");
        }

        public static IEnumerable<T> ApplyOrderBy<T>(IEnumerable<T> entities, LambdaExpression expression)
        {
            return (IEnumerable<T>)orderByMethod.MakeGenericMethod(typeof(T), expression.Type.GetGenericArguments()[1])
                .Invoke(null, new object[] { entities, expression.Compile() });
        }

        private static MethodInfo FindEnumerableMethod(string name)
        {
            return (MethodInfo)typeof(Enumerable).FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Static,
                delegate(MemberInfo m, object o)
                {
                    if (m.Name != name)
                        return false;

                    ParameterInfo[] parameters = ((MethodInfo)m).GetParameters();

                    return parameters.Length == 2 && parameters[0].Name == "source" && parameters[1].Name == "keySelector";
                }, null).Single();
        }

        public static LambdaExpression CreateLambdaExpression(string propertyName, Type entityType)
        {
            ParameterExpression parameterExpression = Expression.Parameter(entityType, "it");
            MemberExpression memberExpression = Expression.PropertyOrField(parameterExpression, propertyName);

            // We need to create a type for the delegate which will be always take
            // a single argument and return a single value so its a Func<T,TResult> type
            Type delegateType = typeof(Func<,>).MakeGenericType(entityType, memberExpression.Type);

            return Expression.Lambda(delegateType, memberExpression, parameterExpression);
        }
    }
}