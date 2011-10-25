using System.Web.Mvc;

namespace Bennington.Core.Extensions
{
    public static class MetadataExtensions
    {
        public static bool IsHidden(this ModelMetadata metadata)
        {
            return metadata.AdditionalValues.ContainsKey("Hidden") && true.Equals(metadata.AdditionalValues["Hidden"]);
        }

        public static bool IsSearchable(this ModelMetadata metadata)
        {
            return metadata.AdditionalValues.ContainsKey("Searchable") && true.Equals(metadata.AdditionalValues["Searchable"]);
        }
    }
}