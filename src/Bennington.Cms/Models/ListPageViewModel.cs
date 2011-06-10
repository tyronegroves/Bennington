using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Bennington.Cms.Buttons;
using MvcTurbine.ComponentModel;
using MvcTurbine.Web.Metadata;

namespace Bennington.Cms.Models
{
    [LoadButtonsFromRegistry]
    public class ListPageViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
    }

    public class LoadButtonsFromRegistryAttribute : MetadataAttribute
    {

    }

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

            metadata.AdditionalValues["TopRightButtons"] = buttonRetriever.GetButtonsForTopRightOfListPage(type) as IEnumerable<Button>;
        }
    }

    public interface IButtonRetriever
    {
        IEnumerable<Button> GetButtonsForTopRightOfListPage(Type modelType);
    }

    public class ButtonRetriever : IButtonRetriever
    {
        private readonly IServiceLocator serviceLocator;

        public ButtonRetriever(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public IEnumerable<Button> GetButtonsForTopRightOfListPage(Type modelType)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (var type in assembly.GetTypes().Where(x=>x.IsInterface == false && x.IsAbstract == false))
                {
                    var interfaces = type.GetInterfaces()
                        .FirstOrDefault(y => y.Name.StartsWith("IListPageListPageButtonRegistry`"));

                    if (interfaces == null) continue;
                    var genericArguments = interfaces.GetGenericArguments();
                    if (genericArguments.Any() && genericArguments[0] == modelType)
                        return ((IListPageButtonRegistry) serviceLocator.Resolve(type)).GetTheTopRightButtons();
                }
            return new Button[] {};
        }
    }
}