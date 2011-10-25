using System;
using System.Web.Mvc;

namespace Bennington.Cms.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class NoIdentifierAttribute : Attribute, IMetadataAware
    {
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("NoIdentifier", true);
        }
    }
}