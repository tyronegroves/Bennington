using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class HiddenAttribute : MetadataAttribute
    {
    }

    public class HiddenAttributeHandler : IMetadataAttributeHandler<HiddenAttribute>
    {
        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            metadata.TemplateHint = "Hidden";
        }
    }
}