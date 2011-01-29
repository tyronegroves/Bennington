using MvcTurbine.ComponentModel;
using Paragon.ContentTree.TreeNodeExtensionProvider;

namespace Paragon.ContentTree.ExampleEngineNodeProvider
{
	public class ContentTreeContactUsTreeNodeExtensionProviderRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			var typeName = typeof(ExampleEngineNodeProvider).Name;
			locator.Register<IAmATreeNodeExtensionProvider, ExampleEngineNodeProvider>(typeName);
		}
	}
}
