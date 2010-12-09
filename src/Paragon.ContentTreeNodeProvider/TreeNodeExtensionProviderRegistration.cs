using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTreeNodeProvider
{
	public class TreeNodeExtensionProviderRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			var typeName = typeof(ContentTreeNodeExtensionProvider).Name;
			locator.Register<IAmATreeNodeExtensionProvider, ContentTreeNodeExtensionProvider>(typeName);
		}
	}
}
