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
            //var type = args.ModelType.GetGenericArguments()[0];
            var type = args.ModelType;
            var @object = args.ModelAccessor();

            metadata.AdditionalValues["ActionButtons"] = editPageButtonRetriever.GetActionButtons(type, @object);
        }
    }
}