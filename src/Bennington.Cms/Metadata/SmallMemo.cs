using System.Web.Mvc;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class SmallMemoAttribute : TextareaAttribute
    {
    }

    public class SmallMemoAttributeHandler : IMetadataAttributeHandler<SmallMemoAttribute>
    {
        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            metadata.TemplateHint = "SmallMemo";
        }
    }
}