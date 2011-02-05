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

			ServiceLocatorManager.SetLocatorProvider(() => locator);
		}
	}
}