using System.Configuration;
using Microsoft.Practices.Unity;
using MvcTurbine.ComponentModel;
using MvcTurbine.Unity;
using MvcTurbine.Web;
using Paragon.ContentTreeSectionNodeProvider.Data;

namespace DEGSMS4
{
	public class MvcApplication : TurbineApplication
	{
		public MvcApplication()
		{
			var locator = new UnityServiceLocator();

			RegisterContentTreeDataContext(locator);
			RegisterContentTreeNodeProviderDataContext(locator);

			var injectedConnectionString = new InjectionConstructor(string.Empty);
			((IUnityContainer)locator.Container).RegisterType<ContentTreeSectionNodeProviderDataModelDataContext>(injectedConnectionString);

			ServiceLocatorManager.SetLocatorProvider(() => locator);
		}

		private static void RegisterContentTreeNodeProviderDataContext(UnityServiceLocator locator)
		{
			var injectedConnectionString = new InjectionConstructor(string.Empty);
			((IUnityContainer)locator.Container).RegisterType<Paragon.ContentTreeNodeProvider.Data.ContentTreeNodeProviderDataModelDataContext>(injectedConnectionString);
		}

		private static void RegisterContentTreeDataContext(UnityServiceLocator locator)
		{
			var injectedConnectionString = new InjectionConstructor(string.Empty);
			((IUnityContainer)locator.Container).RegisterType<Paragon.ContentTree.Data.DataModelDataContext>(injectedConnectionString);
		}
	}
}