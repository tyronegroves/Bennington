using System;
using System.Web.Mvc;

namespace Bennington.Cms.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class IdentifierAttribute : Attribute, IMetadataAware
    {
        private readonly string propertyName;

        public IdentifierAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("Identifier", propertyName);
        }
    }
}