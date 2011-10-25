using System;
using System.Web.Mvc;

namespace Bennington.Core.List
{
    public class HiddenAttribute : Attribute, IMetadataAware
    {
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["Hidden"] = true;
            metadata.TemplateHint = "Hidden";
            metadata.HideSurroundingHtml = true;
        }
    }
}