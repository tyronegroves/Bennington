using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.SectionNodeProvider
{
	public class TreeNodeExtensionProviderRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			var typeName = typeof(SectionNodeProvider).Name;
			locator.Register<IAmATreeNodeExtensionProvider, SectionNodeProvider>(typeName);
		}
	}
}
