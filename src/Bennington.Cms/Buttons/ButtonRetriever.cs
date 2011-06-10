using System;
using System.Collections.Generic;
using System.Linq;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.Buttons
{
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

        public IEnumerable<Button> GetButtonsForBottomLeftOfListPage(Type modelType)
        {
            var buttonRegistryType = GetTheButtonRegistryForThisType(modelType);

            return ThisIsNotAValidButtonRegistryType(buttonRegistryType)
                       ? AnEmptySet()
                       : CreateTheButtonRegistry(buttonRegistryType).GetTheBottomRightButtons();
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