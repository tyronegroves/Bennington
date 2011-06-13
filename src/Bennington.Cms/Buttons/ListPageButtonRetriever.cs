using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcTurbine.ComponentModel;

namespace Bennington.Cms.Buttons
{
    public interface IListPageButtonRetriever
    {
        IEnumerable<Button> GetButtonsForTopRightOfListPage(Type modelType);
        IEnumerable<Button> GetButtonsForBottomLeftOfListPage(Type modelType);
    }

    public class ListPageButtonRetriever : IListPageButtonRetriever
    {
        private readonly IServiceLocator serviceLocator;

        public ListPageButtonRetriever(IServiceLocator serviceLocator)
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
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var buttonRegistryType = GetButtonRegistryType_OrBust(assembly, modelType);
                if (buttonRegistryType != null) return buttonRegistryType;
            }
            return null;
        }

        private static Type GetButtonRegistryType_OrBust(Assembly assembly, Type modelType)
        {
            try
            {
                return GetButtonRegistryType(assembly, modelType);
            }
            catch
            {
                return null;
            }
        }

        private static Type GetButtonRegistryType(Assembly assembly, Type modelType)
        {
            foreach (var type in assembly.GetTypes().Where(x => x.IsInterface == false && x.IsAbstract == false))
            {
                var interfaces = type.GetInterfaces()
                    .FirstOrDefault(y => y.Name.StartsWith("IListPageButtonRegistry`"));

                if (interfaces == null) continue;
                var genericArguments = interfaces.GetGenericArguments();
                if (genericArguments.Any() && genericArguments[0] == modelType)
                    return type;
            }
            return null;
        }
    }
}