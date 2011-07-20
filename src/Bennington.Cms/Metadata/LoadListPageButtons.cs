using System;
using System.Linq;
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
            try
            {
                var type = GetTheListPageViewModelType(args);

                metadata.AdditionalValues["TopRightButtons"] = listPageButtonRetriever.GetButtonsForTopRightOfListPage(type);
                metadata.AdditionalValues["BottomLeftButtons"] = listPageButtonRetriever.GetButtonsForBottomLeftOfListPage(type);
            } catch
            {
                metadata.AdditionalValues["TopRightButtons"] = new Button[] {};
                metadata.AdditionalValues["BottomLeftButtons"] = new Button[] {};
            }
        }

        private static Type GetTheListPageViewModelType(CreateMetadataArguments args)
        {
            return args.ModelType.GetGenericArguments().Any() 
                ? args.ModelType.GetGenericArguments()[0] 
                : args.ModelType.BaseType.GetGenericArguments()[0];
        }
    }
}