using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Bennington.Cms.Tests
{
    public static class ValueProviderFactoresExtensions
    {
        public static ValueProviderFactoryCollection ReplaceWith<TOriginal>(this ValueProviderFactoryCollection factories, Func<ControllerContext, NameValueCollection> sourceAccessor)
        {
            var original = factories.FirstOrDefault(type => typeof(TOriginal) == type.GetType());
            
            if (original != null)
            {
                var index = factories.IndexOf(original);

                factories[index] = new TestValueProviderFactory(sourceAccessor);
            }

            return factories;
        }

        private class TestValueProviderFactory : ValueProviderFactory
        {
            private readonly Func<ControllerContext, NameValueCollection> sourceAccessor;

            public TestValueProviderFactory(Func<ControllerContext, NameValueCollection> sourceAccessor)
            {
                this.sourceAccessor = sourceAccessor;
            }

            public override IValueProvider GetValueProvider(ControllerContext controllerContext)
            {
                return new NameValueCollectionValueProvider(sourceAccessor(controllerContext), CultureInfo.CurrentCulture);
            }
        }
    }
}