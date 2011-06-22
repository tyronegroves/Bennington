using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class HtmlEditorAttribute : MetadataAttribute
    {
    }

    public class HtmlEditorAttributeHandler : IMetadataAttributeHandler<HtmlEditorAttribute>
    {
        public virtual void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            metadata.TemplateHint = "HtmlEditor";
        }
    }
}