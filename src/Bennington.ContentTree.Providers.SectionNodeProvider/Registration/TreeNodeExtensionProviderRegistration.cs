using Bennington.ContentTree.TreeNodeExtensionProvider;
using MvcTurbine.ComponentModel;

namespace Bennington.ContentTree.Providers.SectionNodeProvider.Registration
{
	public class TreeNodeExtensionProviderRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, SectionNodeProvider>(typeof(SectionNodeProvider).Name);
		}
	}
}
