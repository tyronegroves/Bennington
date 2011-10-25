using System;
using System.Globalization;
using System.Web.Mvc;

namespace Bennington.Core.Extensions
{
    public static class ValueProviderExtensions
    {
        public static T GetValue<T>(this IValueProvider valueProvider, string name)
        {
            return (T)GetValue(valueProvider, name, typeof(T));
        }

        public static object GetValue(this IValueProvider valueProvider, string name, Type type)
        {
            var valueProviderResult = valueProvider.GetValue(name);

            return valueProviderResult == null ? null : valueProviderResult.ConvertTo(type, CultureInfo.CurrentCulture);
        }
    }
}