using Microsoft.Practices.Unity;
using MvcTurbine.ComponentModel;
using MvcTurbine.Web;
using Paragon.ContentTree.SectionNodeProvider.Data;
using UnityServiceLocator = MvcTurbine.Unity.UnityServiceLocator;

namespace Webroot
{
	public class MvcApplication : TurbineApplication
	{
		public MvcApplication()
		{
			var locator = new UnityServiceLocator();

			RegisterContentTreeDataContext(locator);

			var injectedConnectionString = new InjectionConstructor(string.Empty);
			((IUnityContainer)locator.Container).RegisterType<ContentTreeSectionNodeProviderDataModelDataContext>(injectedConnectionString);

			ServiceLocatorManager.SetLocatorProvider(() => locator);
		}

		private static void RegisterContentTreeDataContext(UnityServiceLocator locator)
		{
			var injectedConnectionString = new InjectionConstructor(string.Empty);
			((IUnityContainer)locator.Container).RegisterType<Paragon.ContentTree.Data.DataModelDataContext>(injectedConnectionString);
		}
	}
}