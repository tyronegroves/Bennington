using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.ContentNodeProvider
{
	public class TreeNodeExtensionProviderRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			var typeName = typeof(ContentNodeProvider).Name;
			locator.Register<IAmATreeNodeExtensionProvider, ContentNodeProvider>(typeName);
		}
	}
}
