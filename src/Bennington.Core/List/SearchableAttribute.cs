using System;
using System.Web.Mvc;

namespace Bennington.Core.List
{
    public class SearchableAttribute : Attribute, IMetadataAware
    {
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("Searchable", true);
        }
    }
}