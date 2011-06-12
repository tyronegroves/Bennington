using System.Web.Mvc;
using Bennington.Cms.Buttons;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Metadata
{
    public class LoadListPageButtons : MetadataAttribute
    {
    }

    public class LoadListPageButtonsAttributeHandler : IMetadataAttributeHandler<LoadListPageButtons>
    {
        private readonly IListPageButtonRetriever listPageButtonRetriever;

        public LoadListPageButtonsAttributeHandler(IListPageButtonRetriever listPageButtonRetriever)
        {
            this.listPageButtonRetriever = listPageButtonRetriever;
        }

        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            var type = args.ModelType.GetGenericArguments()[0];

            metadata.AdditionalValues["TopRightButtons"] = listPageButtonRetriever.GetButtonsForTopRightOfListPage(type);
            metadata.AdditionalValues["BottomLeftButtons"] = listPageButtonRetriever.GetButtonsForBottomLeftOfListPage(type);
        }
    }
}