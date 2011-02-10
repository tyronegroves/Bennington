using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.SectionNodeProvider.Registration
{
	public class TreeNodeExtensionProviderRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<IAmATreeNodeExtensionProvider, SectionNodeProvider>(typeof(SectionNodeProvider).Name);
		}
	}
}
