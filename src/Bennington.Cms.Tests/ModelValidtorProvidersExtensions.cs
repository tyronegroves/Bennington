using System.Linq;
using System.Web.Mvc;

namespace Bennington.Cms.Tests
{
    public static class ModelValidatorProvidersExtensions
    {
        public static ModelValidatorProviderCollection ReplaceWith<TOriginal>(this ModelValidatorProviderCollection providers, ModelValidatorProvider provider)
        {
            var original = providers.FirstOrDefault(type => typeof(TOriginal) == type.GetType());

            if(original != null)
            {
                var index = providers.IndexOf(original);

                providers[index] = provider;
            }
            else
            {
                providers.Add(provider);
            }

            return providers;
        }
    }
}