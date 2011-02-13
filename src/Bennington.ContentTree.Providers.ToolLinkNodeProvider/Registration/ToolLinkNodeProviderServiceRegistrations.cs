using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;

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