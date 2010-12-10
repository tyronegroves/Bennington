using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.Providers.ContentTreeContactUsNodeProvider
{
	public class ContentTreeContactUsTreeNodeExtensionProviderRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			var typeName = typeof(ContentTreeContactUsNodeProvider).Name;
			locator.Register<IAmATreeNodeExtensionProvider, ContentTreeContactUsNodeProvider>(typeName);
		}
	}
}
