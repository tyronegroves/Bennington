using System.Collections.Generic;
using System.Linq;
using Bennington.ContentTree.TreeNodeExtensionProvider;
using Bennington.Core;

namespace Bennington.ContentTree.Contexts
{
	public interface ITreeNodeProviderContext
	{
		IAmATreeNodeExtensionProvider GetProviderByTypeName(string providerTypeName);
		IEnumerable<IAmATreeNodeExtensionProvider> GetAllTreeNodeProviders();
	}

	public class TreeNodeProviderContext : ITreeNodeProviderContext
	{
		private readonly IServiceLocatorWrapper serviceLocator;

		public TreeNodeProviderContext(IServiceLocatorWrapper serviceLocator)
		{
			this.serviceLocator = serviceLocator;
		}

		public IEnumerable<IAmATreeNodeExtensionProvider> GetAllTreeNodeProviders()
		{
		    var providers = serviceLocator.ResolveServices<IAmATreeNodeExtensionProvider>();
			return providers;
		}

		public IAmATreeNodeExtensionProvider GetProviderByTypeName(string providerTypeName)
		{
			var services = serviceLocator.ResolveServices<IAmATreeNodeExtensionProvider>().Where(a => a.GetType().AssemblyQualifiedName == providerTypeName);
			return services.FirstOrDefault();
		}
	}
}