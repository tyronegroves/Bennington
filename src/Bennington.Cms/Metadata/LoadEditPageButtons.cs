using System.Web.Mvc;
using Bennington.Cms.Buttons;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class LoadEditPageButtons : MetadataAttribute
    {
    }

    public class LoadEditPageButtonsAttributeHandler : IMetadataAttributeHandler<LoadEditPageButtons>
    {
        private readonly IEditPageButtonRetriever editPageButtonRetriever;

        public LoadEditPageButtonsAttributeHandler(IEditPageButtonRetriever editPageButtonRetriever)
        {
            this.editPageButtonRetriever = editPageButtonRetriever;
        }

        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            if (args == null) return;
            if (args.ModelAccessor == null) return;
            var type = args.ModelType;
            var @object = args.ModelAccessor();
            if (@object == null) return;
            metadata.AdditionalValues["ActionButtons"] = editPageButtonRetriever.GetActionButtons(type, @object);
        }
    }
}