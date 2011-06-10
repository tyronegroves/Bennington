using System;
using System.Collections.Generic;
using System.Linq;
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

    public abstract class LoadTheseButtonsForEachRow {
        private readonly IButtonRetriever buttonRetriever;

        protected LoadTheseButtonsForEachRow(IButtonRetriever buttonRetriever)
        {
            this.buttonRetriever = buttonRetriever;
        }

        public virtual IEnumerable<Button> GetButtons()
        {
            return new Button[] {};
        }

        public void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            var model = args.ModelAccessor();
            metadata.AdditionalValues["IndividualRowButtons"] = buttonRetriever.GetButtonsForIndividualRow(args.ModelType, model);
        }
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

            metadata.AdditionalValues["TopRightButtons"] = buttonRetriever.GetButtonsForTopRightOfListPage(type);

        }
    }

    public interface IButtonRetriever
    {
        IEnumerable<Button> GetButtonsForTopRightOfListPage(Type modelType);
        IEnumerable<Button> GetButtonsForIndividualRow(Type type, object model);
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
            var buttonRegistryType = GetTheButtonRegistryForThisType(modelType);

            return ThisIsNotAValidButtonRegistryType(buttonRegistryType)
                       ? AnEmptySet()
                       : CreateTheButtonRegistry(buttonRegistryType).GetTheTopRightButtons();
        }

        public IEnumerable<Button> GetButtonsForIndividualRow(Type type, object model)
        {
            return new[] {new Button {Id = "test", Text = "testing"}};
        }

        private IListPageButtonRegistry CreateTheButtonRegistry(Type buttonRegistryType)
        {
            return ((IListPageButtonRegistry) serviceLocator.Resolve(buttonRegistryType));
        }

        private static IEnumerable<Button> AnEmptySet()
        {
            return new Button[] {};
        }

        private static bool ThisIsNotAValidButtonRegistryType(Type buttonRegistryType)
        {
            return buttonRegistryType == null;
        }

        private static Type GetTheButtonRegistryForThisType(Type modelType)
        {
            Type buttonRegistryType = null;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (var type in assembly.GetTypes().Where(x => x.IsInterface == false && x.IsAbstract == false))
                {
                    var interfaces = type.GetInterfaces()
                        .FirstOrDefault(y => y.Name.StartsWith("IListPageListPageButtonRegistry`"));

                    if (interfaces == null) continue;
                    var genericArguments = interfaces.GetGenericArguments();
                    if (genericArguments.Any() && genericArguments[0] == modelType)
                        buttonRegistryType = type;
                }
            return buttonRegistryType;
        }
    }
}