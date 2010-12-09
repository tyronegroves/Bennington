using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTreeSectionNodeProvider
{
	public class TreeNodeExtensionProviderRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			var typeName = typeof(ContentTreeSectionNodeExtensionProvider).Name;
			locator.Register<IAmATreeNodeExtensionProvider, ContentTreeSectionNodeExtensionProvider>(typeName);
		}
	}
}
