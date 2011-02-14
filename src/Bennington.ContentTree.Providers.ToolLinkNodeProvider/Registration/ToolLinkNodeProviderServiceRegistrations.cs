using Bennington.ContentTree.TreeNodeExtensionProvider;
using MvcTurbine.ComponentModel;

namespace Bennington.ContentTree.Providers.ToolLinkNodeProvider.Registration
{
	public class ToolLinkNodeProviderServiceRegistrations : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, ToolLinkNodeProvider>(typeof(ToolLinkNodeProvider).AssemblyQualifiedName);
		}
	}
}