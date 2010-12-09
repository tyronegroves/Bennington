using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace ContentTreeContactUsNodeProvider
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
