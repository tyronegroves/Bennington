using System.Web.Mvc;
using Bennington.Cms.Buttons;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class LoadButtonsFromRegistryAttributeHandler : IMetadataAttributeHandler<LoadButtonsFromRegistryAttribute>
    {
        private readonly IButtonRetriever buttonRetriever;

        public LoadButtonsFromRegistryAttributeHandler(IButtonRetriever buttonRetriever)
        {
            this.buttonRetriever = buttonRetriever;
        }

        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            var type = args.ModelType.GetGenericArguments()[0];

            metadata.AdditionalValues["TopRightButtons"] = buttonRetriever.GetButtonsForTopRightOfListPage(type);
            metadata.AdditionalValues["BottomLeftButtons"] = buttonRetriever.GetButtonsForBottomLeftOfListPage(type);
        }
    }
}